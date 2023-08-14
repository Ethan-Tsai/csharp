using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;


namespace A_space
{
    public class Program
    {
        /*function: input: string(filepath) output: string  --> read a file,
        convert to zip file with password is systex , and convert to base64string ,
        at last return bas64string*/


        //function_top
        public static class FileZipBase64
        {
            public static string FilePath_In(string filePath)
            {
                //Path format check
                filePath = filePath.Replace("\"", "");

                // 確認檔案存在
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("檔案不存在！", filePath);
                }

                //讀副檔名
                string Ext = System.IO.Path.GetExtension(filePath);

                //for 相對路徑處理
                filePath = Path.GetFullPath(filePath);

                //讀檔名給zip當檔名
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // 讀取檔案內容
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // 將檔案轉換成 ZIP 檔案 -傳檔名、副檔名
                byte[] zipBytes = CompressToZip(fileBytes, Ext, fileName);

                // 將 ZIP 檔案轉換成 Base64 字串
                string base64String = Convert.ToBase64String(zipBytes);

                return base64String;
            }

            //file to zip with password
            private static byte[] CompressToZip(byte[] inputBytes, string ext, string fileName)
            {
                //判斷中英文編碼
                bool containsChinese = ContainsChinese(fileName);
                bool ContainsChinese(string input)
                {
                    var chineseRegex = new Regex(@"[\u4e00-\u9fa5]+");
                    return chineseRegex.IsMatch(input);
                }

                using (MemoryStream outputMemoryStream = new MemoryStream())
                {
                    //中文編碼與英文編碼切換
                    if (!containsChinese)
                    {
                        using (ZipFile zipArchive = new ZipFile())
                        {
                            zipArchive.Password = "systex"; // 設定 ZIP 檔案的密碼

                            // 將檔案加入 ZIP 檔案
                            zipArchive.AddEntry(fileName + ext, inputBytes);

                            //儲存實體zip
                            //zipArchive.Save("Download.zip");

                            // 壓縮 ZIP 檔案並寫入輸出記憶體流中
                            zipArchive.Save(outputMemoryStream);
                        }
                    }
                    else
                    {
                        Encoding utf8WithoutBOM = new UTF8Encoding(false);
                        using (ZipFile zipFile = new ZipFile(utf8WithoutBOM))
                        {
                            zipFile.AlternateEncoding = Encoding.UTF8;
                            zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

                            // 新增檔案進壓縮檔案
                            ZipEntry entry = zipFile.AddEntry(fileName + ext, inputBytes);

                            entry.Password = "systex";

                            // 壓縮 ZIP 檔案並寫入輸出記憶體流中
                            zipFile.Save(outputMemoryStream);
                        }
                    }
                    return outputMemoryStream.ToArray();
                }
            }
        }
        //function_bottom

        public class trytrysee
        {
            public readonly static int time = 1;
            private int sec = 60;
            public void add(int a, int b)
            {
                addtwo(ref a);
                Console.WriteLine(a + b);
                this.sec++;
            }
            private static int addtwo(ref int a)
            {
                a += 20;
                Console.WriteLine(a);
                return (a);
            }
            public void showsec()
            {

                Console.WriteLine(sec);
                Console.WriteLine("小的是: " + min(sec, time));

            }
            private int min(int a, int b)
            {
                return a < b ? a : b;
            }

        }

        public class school
        {
            public readonly static int class_num = 5;
            private int student_num;
            public void stu_num(int a)
            {
                student_num = a;
            }
            public int showStuNum()
            {
                Console.WriteLine(this.name2);
                return this.student_num;

            }
            public static void showlocation()
            {
                Console.WriteLine("中山路" + name);
            }
            private static string name = "systex";
            private string name2 = "學生數量";
            private static string sub = "自然";

            public class teacher
            {
                public static int teacher_num = 10;
                public static void showsubject()
                {
                    Console.WriteLine("老師科目: " + sub);
                }
            }
        }

        public class stu : school
        {
            public string name;
            public stu(string a)
            {
                name = a;
            }
            private int homework
            {
                get { return 0; }
            }

        }

        // 定義 Person 類別作為父類別
        public class Person
        {
            // 屬性：姓名、年齡
            public string Name { get; set; }
            public int Age { get; set; }

            // 建構子
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            // 虛擬方法：顯示基本資訊
            public virtual void DisplayInfo()
            {
                Console.WriteLine($"姓名：{Name}，年齡：{Age} 歲");
            }
        }

        // 定義 Student 類別，繼承自 Person 類別
        public class Student2 : Person
        {
            // 屬性：學號
            public string StudentId { get; set; }

            // 建構子
            public Student2(string name, int age, string studentId) : base(name, age)
            {
                StudentId = studentId;
            }

            // 覆寫父類別的虛擬方法，顯示學生資訊
            public override void DisplayInfo()
            {
                Console.WriteLine($"姓名：{Name}，年齡：{Age} 歲，學號：{StudentId}");
            }
        }

        // 定義 Teacher 類別，繼承自 Person 類別
        public class Teacher : Person
        {
            // 屬性：職稱
            public string Title { get; set; }

            // 建構子
            public Teacher(string name, int age, string title) : base(name, age)
            {
                Title = title;
            }

            // 覆寫父類別的虛擬方法，顯示老師資訊
            public override void DisplayInfo()
            {
                Console.WriteLine($"姓名：{Name}，年齡：{Age} 歲，職稱：{Title}");
            }
        }

        public class coo
        {

            private int name;
            public int Name
            {
                get { return name; }
                set { name = value < 10 ? 10 : value; }
            }
            protected static int age = 3;

        }

        public class coo2 : coo
        {
            public coo2()
            {
                Console.WriteLine("co2_build");
            }
            public int whatage = age;

        }




        public static void Main()
        {
            coo2 Ethanaaa = new coo2();
            Console.WriteLine(Ethanaaa.whatage);
            //string filePath = Console.ReadLine();
            try
            {
                //Console.WriteLine(school.class_num);
                //school s1 = new school();
                //s1.stu_num(100);
                //Console.WriteLine(s1.showStuNum());
                //school.showlocation();
                //Console.WriteLine("老師數量: " + school.teacher.teacher_num);
                //school.teacher.showsubject();
                //stu stu1 = new stu("Ethan");
                //Console.WriteLine(stu1.name);

                //Console.WriteLine(school.teacher.teacher_num);
                //string base64String = FileZipBase64.FilePath_In(filePath);
                //Console.WriteLine(base64String);


                // 创建学生对象，并使用不同的构造函数初始化学生信息
                Student student1 = new Student();
                Student student2 = new Student("Alice", 20, "Computer Science");

                // 使用自动实现属性设置学生的成绩
                student1.MathScore = 85.5;
                student1.EnglishScore = 92.0;
                student2.MathScore = 78.0;
                student2.EnglishScore = 88.5;

                // 计算学生的平均分数
                double averageScore1 = student1.CalculateAverageScore();
                double averageScore2 = student2.CalculateAverageScore();

                // 输出学生信息和平均分数
                Console.WriteLine("Student 1:");
                Console.WriteLine("Name: " + student1.Name);
                Console.WriteLine("Age: " + student1.Age);
                Console.WriteLine("Major: " + student1.Major);
                Console.WriteLine("Math Score: " + student1.MathScore);
                Console.WriteLine("English Score: " + student1.EnglishScore);
                Console.WriteLine("Average Score: " + averageScore1);

                Console.WriteLine();

                Console.WriteLine("Student 2:");
                Console.WriteLine("Name: " + student2.Name);
                Console.WriteLine("Age: " + student2.Age);
                Console.WriteLine("Major: " + student2.Major);
                Console.WriteLine("Math Score: " + student2.MathScore);
                Console.WriteLine("English Score: " + student2.EnglishScore);
                Console.WriteLine("Average Score: " + averageScore2);

                Console.WriteLine();

                // 使用继承创建 StudentWithID 对象，并初始化学生信息
                StudentWithID student3 = new StudentWithID("Bob", 21, "Electrical Engineering", 123456);
                student3.MathScore = 92.5;
                student3.EnglishScore = 85.0;

                double averageScore3 = student3.CalculateAverageScore();

                // 输出带有ID的学生信息和平均分数
                Console.WriteLine("Student 3 (with ID):");
                Console.WriteLine("ID: " + student3.StudentID);
                Console.WriteLine("Name: " + student3.Name);
                Console.WriteLine("Age: " + student3.Age);
                Console.WriteLine("Major: " + student3.Major);
                Console.WriteLine("Math Score: " + student3.MathScore);
                Console.WriteLine("English Score: " + student3.EnglishScore);
                Console.WriteLine("Average Score: " + averageScore3);

                //coo coo1 = new coo();
                //coo1.Name = 3;
                //Console.WriteLine(coo1.Name);

                //// 建立一個 Person 類別的物件，但實際指向 Student 類別
                //Person student = new Student("John Doe", 18, "S123456");
                //// 呼叫 DisplayInfo 方法，實際執行的是 Student 類別的 DisplayInfo 方法
                //student.DisplayInfo();

                //// 建立一個 Person 類別的物件，但實際指向 Teacher 類別
                //Person teacher = new Teacher("Jane Smith", 35, "教授");
                //// 呼叫 DisplayInfo 方法，實際執行的是 Teacher 類別的 DisplayInfo 方法
                //teacher.DisplayInfo();

            }
            catch (Exception ex)
            {
                Console.WriteLine("錯誤：" + ex.Message);
            }
        }
    }


    public class Student
    {
        // 封装：使用自动实现属性来封装学生的基本信息
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }

        // 构造函数：提供不同的构造函数，支持不同的初始化方式
        public Student()
        {
            Name = "Unknown";
            Age = 0;
            Major = "Undeclared";
        }

        public Student(string name, int age, string major)
        {
            Name = name;
            Age = age;
            Major = major;
        }

        // 封装：使用自动实现属性来封装学生的成绩信息
        public double MathScore { get; set; }
        public double EnglishScore { get; set; }

        // 计算学生平均分数
        public double CalculateAverageScore()
        {
            return (MathScore + EnglishScore) / 2;
        }
    }

    // 继承：创建一个新的类 StudentWithID，继承自 Student，并添加学生的ID信息
    public class StudentWithID : Student
    {
        public int StudentID { get; set; }

        public StudentWithID()
        {
            StudentID = 0;
        }

        public StudentWithID(string name, int age, string major, int studentID)
            : base(name, age, major)
        {
            StudentID = studentID;
        }
    }
}





