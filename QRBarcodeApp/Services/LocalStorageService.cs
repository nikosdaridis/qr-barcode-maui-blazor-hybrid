using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeApp.Models;
using System.Reflection;

namespace QRBarcodeApp.Services
{
    public class LocalStorageService(ILocalStorageService localStorageService)
    {
        internal class StorageKeys
        {
            internal const string Codes = "Codes";
            internal const string ActiveTab = "ActiveTab";
        }

        public async Task<CodeModel?> GetCodeAsync(string? id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : (await GetCodesAsync()).FirstOrDefault(code => code.Id == id);
        }

        public async Task<List<CodeModel>> GetCodesAsync()
        {
            return await localStorageService.GetItemAsync<List<CodeModel>>(StorageKeys.Codes) ?? [];
        }

        public async Task<string> SaveCodeAsync(BarcodeResult code, string source)
        {
            return await SaveCodeAsync(code.RawValue, code.BarcodeType.ToString().Split('.').Last(), code.BarcodeFormat.ToString().Split('.').Last(), source);
        }

        public async Task<string> SaveCodeAsync(string value, string type, string format, string source)
        {
            CodeModel newCode = new CodeModel { Value = value, Type = type, Format = format, Source = source, Favorite = false };
            List<CodeModel> codes = await GetCodesAsync();
            codes.Add(newCode);
            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return newCode.Id;
        }

        public async Task<CodeModel?> UpdateCodeAsync(string? id, CodeModel updatedCode)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

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

        public async Task<bool> DeleteCodeAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            List<CodeModel> codes = await GetCodesAsync();

            CodeModel? codeToDelete = codes.FirstOrDefault(code => code.Id == id);

            if (codeToDelete is null)
                return false;

            codes.Remove(codeToDelete);
            await localStorageService.SetItemAsync(StorageKeys.Codes, codes);

            return true;
        }

        public async Task<string> GetActiveTabAsync()
        {
            return await localStorageService.GetItemAsync<string>(StorageKeys.ActiveTab) ?? "";
        }

        public async Task SaveActiveTabAsync(string activeTab)
        {
            await localStorageService.SetItemAsync(StorageKeys.ActiveTab, activeTab);
        }
    }
}
