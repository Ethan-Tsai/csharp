using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using Ionic.Zip;

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
                string Ext = GetFileExtension(filePath);

                // 讀取檔案內容
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // 將檔案轉換成 ZIP 檔案
                byte[] zipBytes = CompressToZip(fileBytes, Ext);

                // 將 ZIP 檔案轉換成 Base64 字串
                string base64String = ConvertToBase64(zipBytes);

                return base64String;
            }

            //file to zip
            private static byte[] CompressToZip(byte[] inputBytes, string ext)
            {
                using (MemoryStream outputMemoryStream = new MemoryStream())
                {
                    using (ZipFile zipArchive = new ZipFile())
                    {
                        zipArchive.Password = "systex"; // 設定 ZIP 檔案的密碼

                        // 將檔案加入 ZIP 檔案
                        zipArchive.AddEntry("Download" + ext, inputBytes);

                        //儲存實體zip
                        //zipArchive.Save("Download.zip");

                        // 壓縮 ZIP 檔案並寫入輸出記憶體流中
                        zipArchive.Save(outputMemoryStream);
                    }
                    return outputMemoryStream.ToArray();
                }
            }

            //Zip to base64
            private static string ConvertToBase64(byte[] inputBytes)
            {
                return Convert.ToBase64String(inputBytes);
            }

            //get副檔名給zip
            public static string GetFileExtension(string fileName)
            {
                // 使用內建函式 Path.GetExtension 取得檔案的副檔名
                string extension = System.IO.Path.GetExtension(fileName);
                return extension;
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



