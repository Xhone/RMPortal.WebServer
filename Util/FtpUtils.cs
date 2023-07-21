using System.Net;

namespace RMPortal.WebServer.Utils
{
    public class FtpUtils
    {
        /// <summary>
        /// 检查是否能正常连接FTP服务器
        /// </summary>
        /// _ftpServerIP   IP +端口号
        /// _ftpUserID 用户名，_ftpPassword 密码
        /// <returns></returns>
        public static bool CheckFtp(string _ftpServerIP, string _ftpUserID, string _ftpPassword)
        {
            try
            {
                FtpWebRequest ftprequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + _ftpServerIP + "/"));
                ftprequest.Credentials = new NetworkCredential(_ftpUserID, _ftpPassword);
                ftprequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftprequest.Timeout = 600000;
                FtpWebResponse ftpResponse = (FtpWebResponse)ftprequest.GetResponse();
                ftpResponse.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="ftpServer"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPassword"></param>
        /// <param name="localFilePath"></param>
        /// <param name="ftpFilePath"></param>
        /// <returns></returns>
        public static bool UploadToFtp(string ftpServer, string ftpUserName, string ftpPassword, string localFilePath, string ftpFilePath)
        {
            bool uploaded = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + ftpFilePath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                byte[] fileContents = File.ReadAllBytes(localFilePath);
                request.ContentLength = fileContents.Length;
                string fileName = Path.GetFileName(localFilePath);
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    uploaded = true;
                    //tb_ResultView.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + fileName + "   上传成功" + "\r\n";
                }
            }
            catch (Exception ex)
            {
                //tb_ResultView.Text += "   上传失败" + ex.Message + "\r\n";
                //throw new Exception("上传失败！");
            }
            return uploaded;
        }






    }
}
