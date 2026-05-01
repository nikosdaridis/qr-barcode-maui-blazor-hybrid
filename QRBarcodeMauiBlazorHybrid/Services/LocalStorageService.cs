using BarcodeScanning;
using Blazored.LocalStorage;
using QRBarcodeMauiBlazorHybrid.Models;

namespace QRBarcodeMauiBlazorHybrid.Services;

public sealed class LocalStorageService(ILocalStorageService localStorageService)
{
    /// <summary>
    /// Keys for local storage.
    /// </summary>
    internal static class StorageKeys
    {
        internal const string CODES = "Codes";
        internal const string ACTIVE_TAB = "ActiveTab";
    }

    /// <summary>
    /// Gets code by Id.
    /// </summary>
    public async Task<CodeModel?> GetCodeAsync(string id) =>
        (await GetCodesAsync()).GetValueOrDefault(id);

    /// <summary>
    /// Gets all codes.
    /// </summary>
    public async Task<Dictionary<string, CodeModel>> GetCodesAsync() =>
         await localStorageService.GetItemAsync<Dictionary<string, CodeModel>>(StorageKeys.CODES) ?? [];

    /// <summary>
    /// Saves code from camera scanner.
    /// </summary>
    public async Task<string> SaveCodeAsync(BarcodeResult code, string source) =>
        await SaveCodeAsync(code.RawValue, code.BarcodeType.ToString(), code.BarcodeFormat.ToString(), source);

    /// <summary>
    /// Saves new code.
    /// </summary>
    public async Task<string> SaveCodeAsync(string value, string type, string format, string source)
    {
        CodeModel newCode = new() { Value = value, Type = type, Format = format, Source = source, Favorite = false };
        Dictionary<string, CodeModel> codes = await GetCodesAsync();
        string id = Guid.CreateVersion7().ToString();
        codes.TryAdd(id, newCode);
        await localStorageService.SetItemAsync(StorageKeys.CODES, codes);

        return id;
    }

    /// <summary>
    /// Updates code by merging non-null properties from <paramref name="updatedCode"/> onto the stored code.
    /// </summary>
    public async Task<CodeModel?> UpdateCodeAsync(string id, CodeModel updatedCode)
    {
        Dictionary<string, CodeModel> codes = await GetCodesAsync();

        if (!codes.TryGetValue(id, out CodeModel? codeToUpdate))
        {
            return null;
        }

        if (updatedCode.Value is not null)
        {
            codeToUpdate.Value = updatedCode.Value;
        }
        if (updatedCode.Type is not null)
        {
            codeToUpdate.Type = updatedCode.Type;
        }
        if (updatedCode.Format is not null)
        {
            codeToUpdate.Format = updatedCode.Format;
        }
        if (updatedCode.Source is not null)
        {
            codeToUpdate.Source = updatedCode.Source;
        }
        if (updatedCode.Favorite is not null)
        {
            codeToUpdate.Favorite = updatedCode.Favorite;
        }

        await localStorageService.SetItemAsync(StorageKeys.CODES, codes);

        return codeToUpdate;
    }

    /// <summary>
    /// Deletes code by Id.
    /// </summary>
    public async Task<bool> DeleteCodeAsync(string id)
    {
        Dictionary<string, CodeModel> codes = await GetCodesAsync();

        if (!codes.Remove(id))
        {
            return false;
        }

        await localStorageService.SetItemAsync(StorageKeys.CODES, codes);
        return true;
    }

    /// <summary>
    /// Gets active tab.
    /// </summary>
    public async Task<string> GetActiveTabAsync() =>
        await localStorageService.GetItemAsync<string>(StorageKeys.ACTIVE_TAB) ?? string.Empty;

    /// <summary>
    /// Saves active tab.
    /// </summary>
    public async Task SaveActiveTabAsync(string activeTab) =>
        await localStorageService.SetItemAsync(StorageKeys.ACTIVE_TAB, activeTab);
}
