using System;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;
using System.Configuration;

namespace File_to_ZIP
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

                // �T�{�ɮצs�b
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("�ɮפ��s�b�I", filePath);
                }



                //Ū���ɦW
                string Ext = System.IO.Path.GetExtension(filePath);

                //for �۹���|�B�z
                filePath = Path.GetFullPath(filePath);

                //Ū�ɦW��zip���ɦW
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // Ū���ɮפ��e
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // �N�ɮ��ഫ�� ZIP �ɮ� -���ɦW�B���ɦW
                byte[] zipBytes = CompressToZip(fileBytes, Ext, fileName);

                // �N ZIP �ɮ��ഫ�� Base64 �r��
                string base64String = Convert.ToBase64String(zipBytes);

                return base64String;
            }

            //file to zip with password
            private static byte[] CompressToZip(byte[] inputBytes, string ext, string fileName)
            {
                //�P�_���^��s�X
                bool containsChinese = ContainsChinese(fileName);
                bool ContainsChinese(string input)
                {
                    var chineseRegex = new Regex(@"[\u4e00-\u9fa5]+");
                    return chineseRegex.IsMatch(input);
                }

                using (MemoryStream outputMemoryStream = new MemoryStream())
                {
                    //����s�X�P�^��s�X����
                    if (!containsChinese)
                    {
                        using (ZipFile zipArchive = new ZipFile())
                        {
                            zipArchive.Password = "systex"; // �]�w ZIP �ɮת��K�X

                            // �N�ɮץ[�J ZIP �ɮ�
                            zipArchive.AddEntry(fileName + ext, inputBytes);

                            //�x�s����zip
                            //zipArchive.Save("Download.zip");

                            // ���Y ZIP �ɮרüg�J��X�O����y��
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

                            // �s�W�ɮ׶i���Y�ɮ�
                            ZipEntry entry = zipFile.AddEntry(fileName + ext, inputBytes);

                            entry.Password = "systex";

                            // ���Y ZIP �ɮרüg�J��X�O����y��
                            zipFile.Save(outputMemoryStream);
                        }
                    }
                    return outputMemoryStream.ToArray();
                }
            }
        }
        //function_bottom



        public static void Main()
        {
            string filePath = Console.ReadLine();
            try
            {
                string base64String = FileZipBase64.FilePath_In(filePath);
                Console.WriteLine(base64String);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���~�G" + ex.Message);
            }
        }
    }
}



