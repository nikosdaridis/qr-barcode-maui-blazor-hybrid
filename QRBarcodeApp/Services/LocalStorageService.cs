using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeApp.Models;
using System.Reflection;

namespace QRBarcodeApp.Services
{
    public sealed class LocalStorageService(ILocalStorageService localStorageService)
    {
        /// <summary>
        /// Keys for local storage
        /// </summary>
        internal sealed class StorageKeys
        {
            internal const string Codes = "Codes";
            internal const string ActiveTab = "ActiveTab";
        }

        /// <summary>
        /// Gets code by Id
        /// </summary>
        public async Task<CodeModel?> GetCodeAsync(string id) =>
            (await GetCodesAsync())[id];

        /// <summary>
        /// Gets all codes
        /// </summary>
        public async Task<Dictionary<string, CodeModel>> GetCodesAsync() =>
             await localStorageService.GetItemAsync<Dictionary<string, CodeModel>>(StorageKeys.Codes) ?? [];

        /// <summary>
        /// Saves code from camera scanner
        /// </summary>
        public async Task<string> SaveCodeAsync(BarcodeResult code, string source) =>
            await SaveCodeAsync(code.RawValue, code.BarcodeType.ToString().Split('.').Last(), code.BarcodeFormat.ToString().Split('.').Last(), source);

        /// <summary>
        /// Saves new code
        /// </summary>
        public async Task<string> SaveCodeAsync(string value, string type, string format, string source)
        {
            CodeModel newCode = new() { Value = value, Type = type, Format = format, Source = source, Favorite = false };
            Dictionary<string, CodeModel> codes = await GetCodesAsync();
            string id = Ulid.NewUlid().ToString();
            codes.TryAdd(id, newCode);
            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return id;
        }

        /// <summary>
        /// Updates code with new properties
        /// </summary>
        public async Task<CodeModel?> UpdateCodeAsync(string id, CodeModel updatedCode)
        {
            Dictionary<string, CodeModel> codes = await GetCodesAsync();
            CodeModel? codeToUpdate = codes[id];

            if (codeToUpdate is null)
                return null;

            foreach (PropertyInfo property in typeof(CodeModel).GetProperties().Where(p => p.GetValue(updatedCode) is not null))
                property.SetValue(codeToUpdate, property.GetValue(updatedCode));

            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return codeToUpdate;
        }

        /// <summary>
        /// Deletes code by Id
        /// </summary>
        public async Task<bool> DeleteCodeAsync(string id)
        {
            Dictionary<string, CodeModel> codes = await GetCodesAsync();

            if (codes.Remove(id))
                await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return true;
        }

        /// <summary>
        /// Gets active tab
        /// </summary>
        public async Task<string> GetActiveTabAsync() =>
            await localStorageService.GetItemAsync<string>(StorageKeys.ActiveTab) ?? string.Empty;

        /// <summary>
        /// Saves active tab
        /// </summary>
        public async Task SaveActiveTabAsync(string activeTab) =>
            await localStorageService.SetItemAsync(StorageKeys.ActiveTab, activeTab);
    }
}
