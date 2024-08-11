using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class GoogleSheetsUnity
{
	private readonly string _sheetId;
	private readonly SheetsService _sheetsService;
	private readonly List<string> _headers = new ();

	public GoogleSheetsUnity(string credentialsPath, string sheetId)
	{
		_sheetId = sheetId;
		GoogleCredential credential;
		using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
		{
			credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
		}
		_sheetsService = new SheetsService(new BaseClientService.Initializer()
		{
			HttpClientInitializer = credential
		});
	}

	public async Task DownloadAndParseSheet(string sheetName, IGoogleSheetParser parser)
	{
		Debug.Log(message: $"Starting downloading sheet (${sheetName})…");
		var range = $"{sheetName}!A1:Z";
		var request = _sheetsService.Spreadsheets.Values.Get(_sheetId, range);
		ValueRange response;
		try
		{
			response = await request.ExecuteAsync();
		}
		catch (Exception e)
		{
			Debug.LogError(message: $"Error retrieving Google Sheets data: {e.Message}");
			return;
		}

		if (response != null && response.Values != null)
		{			
			Debug.Log(message: $"Sheet downloaded successfully: {sheetName}. Parsing");

			var tableArray = response.Values;
			var firstRow = tableArray[0];
			foreach (var cell in firstRow)
			{
				_headers.Add(cell.ToString());
			}

			var rowsCount = tableArray.Count;
			for (var i = 1; i < rowsCount; i++)
			{
				var row = tableArray[i];
				var rowLength = row.Count;
				for (var j = 0; j < rowLength; j++)
				{
					var cell = row[j];
					var header = _headers[j];
					parser.Parse(header, cell.ToString());
					Debug.Log($"Header: {header}, Value: {cell}");
				}
			}
			Debug.Log(message: $"Sheet parsed successfully.");			
		}
		else
		{
			Debug.LogWarning(message: "No data found in Google Sheets.");
		}			
	}
}