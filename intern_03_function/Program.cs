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
                Console.WriteLine("錯誤：" + ex.Message);
            }
        }
    }
}



