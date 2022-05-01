using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SourceGenerator
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var rec = (CustomReceiver) context.SyntaxReceiver;
            if (rec is null || rec.ApiVersions.Count < 1)
                return;

            var versions = rec.Items.Where(x => x.Identifier.ValueText == "Api");
            //foreach (var node in classSyntax.ChildNodes())
            //{
            //    bool isPublicConstant =

            //            if (node is FieldDeclarationSyntax fieldSyntax)
            //    {
            //        Items.Add(fieldSyntax);
            //    }
            //}
            //foreach (var item in collection)
            //{

            //}

            var className = "GeneratedIdentityApiRoutes";
            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("namespace Yoli.WebApi.Routes");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine($"\tpublic static class {className}");
            sourceBuilder.AppendLine("\t{");

            foreach (var version in rec.ApiVersions)
            {
                var identifier = version.Identifier;
                var value = version.Value;
                sourceBuilder.AppendLine($"\t\tpublic const string {identifier} = \"{value}\";");
            }            

            sourceBuilder.AppendLine("\t}");
            sourceBuilder.AppendLine("}");


            context.AddSource(className, SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CustomReceiver());
#if DGENERATORS
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
        }

        public class StringFieldData
        {
            public bool IsValid { get; }
            public string Identifier { get; set; }
            public string Value { get; set; }
            public StringFieldData(string identifier, string value)
            {
                Identifier = identifier;
                Value = value;
                IsValid = !string.IsNullOrWhiteSpace(Identifier);
            }
        }

        public class CustomReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> Items = new List<ClassDeclarationSyntax>();
            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax classSyntax)
                {
                    var valueText = classSyntax.Identifier.ValueText;
                    if (valueText.Equals("ApiVersion"))
                    {
                        var fields = GetPublicConstantFieldsData(classSyntax);
                        if (fields.Count < 1)
                            return;
                        Items.Add(classSyntax);
                        ApiVersions.AddRange(fields);
                    }

                    if (valueText != "ApiRoutes" && valueText.Contains("Routes"))
                    {
                        var fields = GetPublicConstantFieldsData(classSyntax);
                        if (fields.Count < 1)
                            return;

                        Items.Add(classSyntax);
                    }
                }
            }

            public List<StringFieldData> GetPublicConstantFieldsData(ClassDeclarationSyntax syntax)
            {
                var fields = new List<StringFieldData>();
                foreach (var field in syntax.ChildNodes())
                {
                    if (!(field is FieldDeclarationSyntax))
                        continue;

                    var fieldSyntax = field as FieldDeclarationSyntax;

                    bool isPublic = false;
                    bool isConstant = false;
                    foreach (var modifier in fieldSyntax.Modifiers)
                    {
                        if (modifier.ValueText == "public")
                            isPublic = true;
                        else if (modifier.ValueText == "const")
                            isConstant = true;
                    }

                    if (! (isPublic && isConstant && fieldSyntax.Modifiers.Count == 2))
                    {
                        continue;
                    }

                    var data = GetFieldData(fieldSyntax);
                    if (data.IsValid)
                        fields.Add(data);
                }

                return fields;
            }

            public StringFieldData GetFieldData(FieldDeclarationSyntax syntax)
            {
                string identifier = string.Empty;
                string value = null;

                var variables = syntax.Declaration.Variables;
                if (variables.Count > 1)
                    throw new System.Exception($"Invalid number of Variables for field {syntax}");
                if (variables.Count == 0)
                    return new StringFieldData(identifier, value);

                var variable = variables[0];
                var exp = variable.Initializer.Value;
                if (exp.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StringLiteralExpression))
                {
                    identifier = variable.Identifier.ValueText;
                    value = (exp as LiteralExpressionSyntax).Token.ValueText;
                }

                return new StringFieldData(identifier, value);
            }

            public List<StringFieldData> ApiVersions = new List<StringFieldData>();
        }
    }
}