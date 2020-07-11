using System;
using System.IO;

namespace JpgRaperLib
{
    public class RaperWriter
    {
        private const int MAX_BUFFER = 4096;

        private RaperCommon raperCommon = new RaperCommon();

        public void writeJpg(string fileJpg, string filePayload, string fileOutput, bool overwriteOutput) {

            try
            {
                if(File.Exists(fileOutput))
                {
                    if (!overwriteOutput)
                    {
                        throw new Exception("File Exists Overwrite Disabled.");
                    }

                    File.Delete(fileOutput);
                }


                if (raperCommon.getFileType(fileJpg) != RaperCommon.SUPPORT_SIGNATURE.JPG) {
                    throw new Exception("Not Supported JPG (RAW, JFIF, EXIF Only!)");
                }

                RaperCommon.SUPPORT_SIGNATURE payloadSignature = raperCommon.getFileType(filePayload);

                if (payloadSignature != RaperCommon.SUPPORT_SIGNATURE.RAR && payloadSignature != RaperCommon.SUPPORT_SIGNATURE.ZIP) {
                    throw new Exception("Not Supported Payload (RAR, ZIP Only!)");
                }

                BinaryWriter writer = new BinaryWriter(File.Open(fileOutput, FileMode.Create));
                writeFileToBinary(ref writer, fileJpg);
                writeFileToBinary(ref writer, filePayload);

                writer.Close();
                writer.Dispose();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void writeFileToBinary(ref BinaryWriter writer, string file) {

            int read;
            byte[] buffer = new byte[MAX_BUFFER];
            FileStream stream = File.Open(file, FileMode.Open);

            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                writer.Write(buffer, 0, read);
            }

            stream.Close();
            stream.Dispose();
        }

    }
}
