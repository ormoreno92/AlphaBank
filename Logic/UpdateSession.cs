using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using Logic.BankRepublic;
using Newtonsoft.Json;
using Model;
using System.IO;
using Renci.SshNet;

namespace Logic
{
    public class UpdateSession
    {
        private readonly AlphaBankEntities _db = new AlphaBankEntities();
        private readonly wservicewsdl _bank = new wservicewsdl();

        public bool ReleaseData()
        {
            try
            {
                var file = GetFileInfo();
                if (string.IsNullOrEmpty(file)) return false;
                var data = JsonConvert.DeserializeObject<List<DummyObj>>(file);
                UpdateDataBase(data);
                _bank.borrar_archivo("");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetFileInfo()
        {
            string host = @"70.38.64.23";
            string username = "ZjZLBkbxMjBk5fxHqtl6Kw";
            string password = @"GRmnmMi7oT8VEiPWvW";
            string remoteDirectory = "/uploads";
            var pathR = "";
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var dir = Path.GetDirectoryName(path);
            dir += "\\Helpers";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            pathR = dir + "\\downloadedFile.txt";
            using (var sftp = new SftpClient(host, 22, username, password))
            {
                try
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(remoteDirectory);
                    foreach (var file in files)
                        if (file.Name.ToUpper().Equals("CLIENTE.TXT"))
                            using (var fileStream = File.OpenWrite(pathR))
                                sftp.DownloadFile(file.FullName, fileStream);
                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return File.ReadAllLines(pathR)[0].ToString();
        }

        private void UpdateDataBase(List<DummyObj> data)
        {
            foreach (var user in data)
            {
                try
                {
                    var query = "exec dbo.sp_createUser '" + user.id + "', '" + user.nombre + "', '" + user.primer_apellido + "', '" + user.segundo_apellido + "', '"
                    + user.identificacion + "', '" + user.fec_cre + "'";
                    var obj = _db.Database.SqlQuery<object>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
    }
}
