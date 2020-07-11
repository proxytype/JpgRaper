using System;
using System.IO;
using System.Linq;

namespace JpgRaperLib
{
    public class RaperReader
    {
        RaperCommon raperCommon = new RaperCommon();

        public void readJpg(string fileJpg, string fileOutput, bool overwriteOutput)
        {
            if (raperCommon.getFileType(fileJpg) != RaperCommon.SUPPORT_SIGNATURE.JPG)
            {
                throw new Exception("File Type Not Supported!");
            }

            int read;
            byte[] buffer = new byte[2];

            FileStream stream = File.Open(fileJpg, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            long position = -1;

            while ((read = reader.Read(buffer, 0, buffer.Length)) != 0)
            {

                if (buffer.SequenceEqual(raperCommon.SIGNATURE_ZIP))
                {
                    position = stream.Position - raperCommon.SIGNATURE_ZIP.Length;
                    break;
                }
                else
                {

                    if (buffer.SequenceEqual(new byte[] { raperCommon.SIGNATURE_RAR[0], raperCommon.SIGNATURE_RAR[1] }))
                    {
                        read = reader.Read(buffer, 0, buffer.Length);
                        if (buffer.SequenceEqual(new byte[] { raperCommon.SIGNATURE_RAR[2], raperCommon.SIGNATURE_RAR[3] }))
                        {
                            read = reader.Read(buffer, 0, buffer.Length);
                            if (buffer.SequenceEqual(new byte[] { raperCommon.SIGNATURE_RAR[4], raperCommon.SIGNATURE_RAR[5] }))
                            {
                                position = stream.Position - raperCommon.SIGNATURE_RAR.Length;
                                break;
                            }
                        }

                    }

                }

            }

            if (position == -1)
            {
                throw new Exception("Unable to locate Payload in JPG.");
            }

            if (File.Exists(fileOutput))
            {
                if (!overwriteOutput)
                {
                    throw new Exception("File Exists Overwrite Disabled.");
                }

                File.Delete(fileOutput);
            }


            stream.Seek(position, SeekOrigin.Begin);
            buffer = new byte[stream.Length - position];
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileOutput, FileMode.Create))) {
                stream.Read(buffer, 0, buffer.Length);
                writer.Write(buffer);
            }

            stream.Close();
            stream.Dispose();
        }


    }



}

