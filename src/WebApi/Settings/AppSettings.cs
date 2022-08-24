namespace WebApi.Settings
{
    public class AppSettings
    {
    }

    // https://stackoverflow.com/questions/1212742/xml-serialize-generic-list-of-serializable-objects
    [System.Xml.Serialization.XmlInclude(typeof(Test2))]
    public class Test
    {
        [System.Xml.Serialization.XmlArray("MyList")]
        [System.Xml.Serialization.XmlArrayItem("TestItem")]
        public List<Test2> List { get; set; } = new List<Test2>();
    }

    public class Test2
    {
        public string Name { get; set; } = "2";
        public Test3 Test3 { get; set; }
    }

    public class Test3
    {
        public string Name { get; set; }
    }
}
