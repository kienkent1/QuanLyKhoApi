using IronBarCode;
using IronSoftware.Drawing;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace QuanLyKhoApi.Helper
{
    public class Ironbarcode
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
            string safeId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Id));

            var barcode = BarcodeWriter.CreateBarcode(safeId, BarcodeEncoding.Code128, 400, 200);
            barcode.SetMargins(10);

            var base64 = barcode.ToPngBinaryData();
            return Convert.ToBase64String(base64);

        }
    }
}
