using HotelInfoServer.Models;

namespace HotelInfoServer.OutputWriters
{
    public abstract class HotelInfoOutputWriterBase: IHotelInfoOutputWriter
    {
        private string _outputExtension { get; set; }

        public HotelInfoOutputWriterBase(string outputExtension)
        {
            //format specific file extension
            this._outputExtension = outputExtension;
        }

        //generates content and writes to output path
        public void WriteToOutputFile(string filePath, HotelInfo[] hotelInfos)
        {
            string content = GenerateOutputContent(hotelInfos);
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + "." + _outputExtension, true))
            {
                file.Write(content);
            }
        }

        //this method will be implemented by inherited childs according to it's own format
        public abstract string GenerateOutputContent(HotelInfo[] hotelInfos);   
    }
}
