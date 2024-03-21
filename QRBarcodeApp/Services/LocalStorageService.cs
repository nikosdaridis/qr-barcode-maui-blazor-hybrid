using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeApp.Models;
using System.Reflection;

namespace QRBarcodeApp.Services
{
    public class LocalStorageService(ILocalStorageService localStorageService)
    {
        /// <summary>
        /// Keys for local storage
        /// </summary>
        internal class StorageKeys
        {
            internal const string Codes = "Codes";
            internal const string ActiveTab = "ActiveTab";
        }

        /// <summary>
        /// Retrieves code by Id
        /// </summary>
        public async Task<CodeModel?> GetCodeAsync(string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : (await GetCodesAsync()).FirstOrDefault(code => code.Id == id);
        }

        /// <summary>
        /// Retrieves all codes
        /// </summary>
        public async Task<List<CodeModel>> GetCodesAsync()
        {
            return await localStorageService.GetItemAsync<List<CodeModel>>(StorageKeys.Codes) ?? [];
        }

        /// <summary>
        /// Saves code from camera scanner
        /// </summary>
        public async Task<string> SaveCodeAsync(BarcodeResult code, string source)
        {
            return await SaveCodeAsync(code.RawValue, code.BarcodeType.ToString().Split('.').Last(), code.BarcodeFormat.ToString().Split('.').Last(), source);
        }

        /// <summary>
        /// Creates and saves new code
        /// </summary>
        public async Task<string> SaveCodeAsync(string value, string type, string format, string source)
        {
            CodeModel newCode = new CodeModel { Value = value, Type = type, Format = format, Source = source, Favorite = false };
            List<CodeModel> codes = await GetCodesAsync();
            codes.Add(newCode);
            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return newCode.Id;
        }

        /// <summary>
        /// Updates code with new properties ignoring Id
        /// </summary>
        public async Task<CodeModel?> UpdateCodeAsync(string id, CodeModel updatedCode)
        {
            List<CodeModel> codes = await GetCodesAsync();
            CodeModel? codeToUpdate = codes.FirstOrDefault(code => code.Id == id);

            if (codeToUpdate is null)
                return null;

            foreach (PropertyInfo property in typeof(CodeModel).GetProperties().Where(p => p.Name != "Id"))
            {
                object? newValue = property.GetValue(updatedCode);

                if (newValue is not null)
                    property.SetValue(codeToUpdate, newValue);
            }

            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return codeToUpdate;
        }

        /// <summary>
        /// Deletes code by Id
        /// </summary>
        public async Task<bool> DeleteCodeAsync(string id)
        {
            List<CodeModel> codes = await GetCodesAsync();
            CodeModel? codeToDelete = codes.FirstOrDefault(code => code.Id == id);

            if (codeToDelete is null)
                return false;

            codes.Remove(codeToDelete);
            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return true;
        }

        /// <summary>
        /// Retrieves active tab
        /// </summary>
        public async Task<string> GetActiveTabAsync()
        {
            return await localStorageService.GetItemAsync<string>(StorageKeys.ActiveTab) ?? "";
        }

        /// <summary>
        /// Saves active tab
        /// </summary>
        public async Task SaveActiveTabAsync(string activeTab)
        {
            await localStorageService.SetItemAsync(StorageKeys.ActiveTab, activeTab);
        }
    }
}
