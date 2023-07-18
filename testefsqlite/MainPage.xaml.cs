using Microsoft.EntityFrameworkCore;
using SqliteClassLibrary;
using SqliteClassLibrary.Models;
using System.Linq;

namespace testefsqlite;

public partial class MainPage : ContentPage
{
	int counter = 0;
    private readonly ApplicationDBContext _context;

    public MainPage(ApplicationDBContext context)
	{
		InitializeComponent();
		_context = context;
		GetItems();

    }

	private async void OnAddClicked(object sender, EventArgs e)
	{
		counter++;
		var newItem = new Item
		{
			Name = $"Item {counter}"
		};

		try
		{
			await _context.Items.AddAsync(newItem);
			await _context.SaveChangesAsync();
			testlbl.Text = $"{newItem.Name} has been successfully added.";
            GetItems();
        }
		catch (Exception ex)
		{
			testlbl.Text = ex.Message;
		}
	}

    private void OnClearClicked(object sender, EventArgs e)
	{
        var data = _context.Items.ToArray();
        _context.Items.RemoveRange(data);
		_context.SaveChanges();
        testlbl.Text = $"Items has been successfully cleared.";
        GetItems();
    }


    private async void GetItems()
	{
		var data = await _context.Items.ToListAsync();

		if (data != null && data.Count > 0)
		{
            lvItem.ItemsSource = data;
        }
		else
		{
            lvItem.ItemsSource = null;
        }

    }
}

