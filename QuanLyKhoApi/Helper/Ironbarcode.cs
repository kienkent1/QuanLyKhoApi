using IronBarCode;
using Microsoft.AspNetCore.Mvc.Filters;
using IronSoftware.Drawing;

namespace QuanLyKhoApi.Helper
{
    public  class Ironbarcode
    {
        private readonly string key;

        public Ironbarcode(IConfiguration con)
        {
           IronBarCode.License.LicenseKey = con["IronbarcodeKey"];
            key = con["IronbarcodeKey"];
        }


        public async Task<string> ReadBarcode(IFormFile file)
        {
            var options = new BarcodeReaderOptions
            {
                Speed = ReadingSpeed.ExtremeDetail,
                ExpectBarcodeTypes = BarcodeEncoding.All,
                RemoveFalsePositive = true,
                AutoRotate = true,
                ConfidenceThreshold = 0.6,
                Multithreaded = true,
                MaxParallelThreads = 4,
                ImageFilters = new ImageFilterCollection()
            };

            var tempPath = Path.GetTempFileName();
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var myBarcodes = BarcodeReader.Read(tempPath, options);
            string result = string.Join(" ", myBarcodes.Select(x => x.Value));
            File.Delete(tempPath);

            return result;
        }
        public async Task<string> GeneratedBarcode(string Id)
        {
            var barcode =  BarcodeWriter.CreateBarcode(Id, BarcodeEncoding.Code128, 200, 100);
            var base64 = barcode.ToPngBinaryData();
            return Convert.ToBase64String(base64);

        }
    }
}
