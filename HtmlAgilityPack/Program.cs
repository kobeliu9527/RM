using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlAgilityPack
{
    internal class Program
    {
        public class ClassInfo
        {
            public string Name { get; set; } = "na";
            public string Descri { get; set; } = "asdf";
            public List<(string Type, string Name, string Summary)> ClassProps { get; set; } = new List<(string Type, string Name, string Summary)>();
        }
        static void GetClass(HtmlNode node, List<ClassInfo> list,params  string[] classNames)
        {
            ClassInfo classInfo=new ClassInfo();
           // var classNames= Names.Split(' ');
            string temp = "//div[contains(@class, '";
            for (int i = 0; i < classNames.Length-1; i++) 
            {
                temp += classNames[i] + "') and contains(@class, '";
            }
            temp += classNames.Last()+ "')]";
           
            var firstClassNodes = node.SelectNodes(temp);
            if (firstClassNodes != null)
            {
                foreach (var firstnodeclass in firstClassNodes)
                {
                    //找到属性名
                    string id = firstnodeclass.GetAttributeValue("id", "No ID");
                    string pname = id.Substring(id.LastIndexOf('-') + 1);
                    //找到属性描述 
                    var itemDescriptionNode = firstnodeclass.SelectSingleNode(".//div[@class='item-description']");
                    var itemDescriptionNode2 = firstnodeclass.SelectSingleNode(".//div[@class='prop-types']");
                    if (itemDescriptionNode != null)
                    {
                        var paragraphNodes = itemDescriptionNode.SelectNodes(".//p");
                        string dec = "";
                        if (paragraphNodes != null)
                        {
                            foreach (var paragraphNode in paragraphNodes)
                            {
                                dec += paragraphNode.InnerText.Replace(" ", "").Replace("\r\n", "");
                            }
                        }

                        //Console.WriteLine(dec);
                        classInfo.ClassProps.Add(("JsVal?", pname, dec));
                    }
                    if (itemDescriptionNode2 != null)
                    {
                        var paragraphNodes = itemDescriptionNode2.SelectNodes(".//span");
                        string dec = "";
                        if (paragraphNodes != null)
                        {
                            foreach (var paragraphNode in paragraphNodes)
                            {
                                dec += paragraphNode.InnerText.Replace(" ", "").Replace("\r\n", "");
                            }

                        }
                        Console.WriteLine($"类型" + dec);
                    }

                }
            }
            else
            {
                Console.WriteLine("No nodes with class 'doc-content-item-card level-1 leaf' found.");
            }
            var ulNodes = node.SelectNodes("//ul");

            if (ulNodes != null)
            {
                foreach (var ulNode in ulNodes)
                {
                    // Find all <li> tags under each <ul> node
                    var liNodes = ulNode.SelectNodes(".//li");

                    if (liNodes != null)
                    {
                        foreach (var liNode in liNodes)
                        {
                            // Find <code> tags under each <li> node
                            var codeNode = liNode.SelectSingleNode(".//code");

                            if (codeNode != null)
                            {
                                // Print the content of each <code> tag
                                Console.WriteLine(codeNode.InnerText);
                            }
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("No <ul> tags found.");
            }

            list.Add(classInfo);
        }
        static void Main(string[] args)
        {
            
            var generator = new ClassGenerator();
            List<ClassInfo> classes = new List<ClassInfo>();

            List<List<(string Type, string Name, string Summary)>> ps = new List<List<(string Type, string Name, string Summary)>>();
            List<(string Type, string Name, string Summary)> props = new List<(string Type, string Name, string Summary)>
            {
                //("int", "Id", "Gets or sets the ID.")
            };

            string className = "";

            var html = File.ReadAllText("htm.html");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            GetClass(doc.DocumentNode, classes, "doc-content-item-card", "level-1", "leaf");
            string code = "";
            foreach (var item in classes)
            {
                  code = generator.GenerateClass("SharedPage.Model", item.Name, item.ClassProps, item.Descri);
                
            }

            ;
            className = doc.DocumentNode.SelectSingleNode("//h2").InnerText.Replace(" ", "").Replace("\r\n", "");

            HtmlNode firstPageDescriptionNode = doc.DocumentNode.SelectSingleNode("//div[@class='page-description']");
            var firstParagraphNode = firstPageDescriptionNode.SelectSingleNode(".//p");
            var classDes = firstParagraphNode.InnerText.Replace(" ", "").Replace("\r\n", "");
            ClassInfo first = new ClassInfo();
            first.Name = className;
            first.Descri = classDes;
            // 查找所有class为doc-content-item-card level-1 leaf的节点
            var firstClassNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'doc-content-item-card') and contains(@class, 'level-1') and contains(@class, 'leaf')]");
            if (firstClassNodes != null)
            {
                foreach (var firstnodeclass in firstClassNodes)
                {
                    //找到属性名
                    string id = firstnodeclass.GetAttributeValue("id", "No ID");
                    string pname = id.Substring(id.LastIndexOf('-') + 1);
                    //找到属性描述 
                    var itemDescriptionNode = firstnodeclass.SelectSingleNode(".//div[@class='item-description']");
                    var itemDescriptionNode2 = firstnodeclass.SelectSingleNode(".//div[@class='prop-types']");
                    if (itemDescriptionNode != null)
                    {
                        var paragraphNodes = itemDescriptionNode.SelectNodes(".//p");
                        string dec = "";
                        if (paragraphNodes != null)
                        {
                            foreach (var paragraphNode in paragraphNodes)
                            {
                                dec += paragraphNode.InnerText.Replace(" ", "").Replace("\r\n", "");
                            }
                        }

                        //Console.WriteLine(dec);
                        first.ClassProps.Add(("JsVal?", pname, dec));
                    }
                    if (itemDescriptionNode2 != null)
                    {
                        var paragraphNodes = itemDescriptionNode2.SelectNodes(".//span");
                        string dec = "";
                        if (paragraphNodes != null)
                        {
                            foreach (var paragraphNode in paragraphNodes)
                            {
                                dec += paragraphNode.InnerText.Replace(" ", "").Replace("\r\n", "");
                            }

                        }
                        Console.WriteLine($"类型" + dec);
                    }

                }
            }
            else
            {
                Console.WriteLine("No nodes with class 'doc-content-item-card level-1 leaf' found.");
            }
            var ulNodes = doc.DocumentNode.SelectNodes("//ul");

            if (ulNodes != null)
            {
                foreach (var ulNode in ulNodes)
                {
                    // Find all <li> tags under each <ul> node
                    var liNodes = ulNode.SelectNodes(".//li");

                    if (liNodes != null)
                    {
                        foreach (var liNode in liNodes)
                        {
                            // Find <code> tags under each <li> node
                            var codeNode = liNode.SelectSingleNode(".//code");

                            if (codeNode != null)
                            {
                                // Print the content of each <code> tag
                                Console.WriteLine(codeNode.InnerText);
                            }
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("No <ul> tags found.");
            }
            string code2 = generator.GenerateClass("SharedPage.Model", className, props, classDes);
            Console.WriteLine(code);
        }
    }
    public class ClassGenerator
    {
        public string GenerateClass(string namespaceName, string className, List<(string Type, string Name, string Summary)> properties, string classSummary)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();

            // Add using directives
            var usingDirective = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System"));
            compilationUnit = compilationUnit.AddUsings(usingDirective);

            // Define the namespace
            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName)).NormalizeWhitespace();

            // Define the class
            var classDeclaration = SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .WithLeadingTrivia(CreateSummaryTrivia(classSummary)); // Add class summary

            // Add properties to the class
            foreach (var property in properties)
            {
                var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(property.Type), property.Name)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                    .WithLeadingTrivia(CreatePropertySummary(property.Summary)); // Add property summary

                classDeclaration = classDeclaration.AddMembers(propertyDeclaration);
            }

            // Add the class to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            // Add the namespace to the compilation unit
            compilationUnit = compilationUnit.AddMembers(namespaceDeclaration);

            // Normalize and get code as string
            var code = compilationUnit.NormalizeWhitespace().ToFullString();
            return code;
        }

        private SyntaxTriviaList CreateSummaryTrivia(string summaryText)//类
        {
            var summaryContent = $"    /// <summary>\r\n    ///{summaryText} \r\n    /// </summary>\r\n    ";
            var summaryXml = SyntaxFactory.ParseLeadingTrivia(summaryContent);
            return SyntaxFactory.TriviaList(summaryXml);
        }

        private SyntaxTriviaList CreatePropertySummary(string summaryText)//属性
        {
            var summaryContent = $"    /// <summary>\r\n    ///{summaryText} \r\n    /// </summary>\r\n    ";
            var summaryXml = SyntaxFactory.ParseLeadingTrivia(summaryContent);
            return SyntaxFactory.TriviaList(summaryXml);
        }
    }
}
