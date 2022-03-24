using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Maui.Essentials;
using Microsoft.EntityFrameworkCore;

using TodoMaui.DbContexts;
using TodoMaui.Entities;
using TodoMaui.Mvvm;

namespace TodoMaui;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<TodoItem> Todos { get; init; } = new(); 
    
    private string _Note;

    public string Note
    {
        get
        {
            return _Note;
        }

        set
        {
            _Note = value;

            OnPropertyChanged(null);
        }
    }

    public DelegateCommand ClickAddTodoCommand { get; init; }

    public DelegateCommand<TodoItem> OnTodosSelectionCommand { get; init; }

    public IServiceProvider ServiceProvider { get; init; }

    public TodoMauiDbContext TodoMauiDbContext { get; init; }

    public MainPageViewModel(IServiceProvider serviceProvider, TodoMauiDbContext todoMauiDbContext)
    {
        ServiceProvider = serviceProvider;

        TodoMauiDbContext = todoMauiDbContext;

        ClickAddTodoCommand = new DelegateCommand(async () => await OnAddTodoAsync());

        OnTodosSelectionCommand = new DelegateCommand<TodoItem>(async (selectedItem) =>
        {
            MainPage mainPage = serviceProvider.GetService<MainPage>();

            string action = await mainPage.DisplayActionSheet ("Action", "Cancel", null, "Toggle Done");

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                TodoItem todoItem = selectedItem as TodoItem;

                TodoEntity todoEntity = await TodoMauiDbContext.Todos
                    .Where(v => v.TodoId == todoItem.TodoId)
                    .SingleAsync();

                todoEntity.IsDone = !todoEntity.IsDone;

                await TodoMauiDbContext.SaveChangesAsync();

                LoadTodos();
            });
        });

        LoadTodos();
    }

    public async Task OnAddTodoAsync()
    {
        TodoMauiDbContext.Todos.Add(new TodoEntity
        {
            Note = Note,
            IsDone = false
        });

        await TodoMauiDbContext.SaveChangesAsync();

        LoadTodos();
    }

    public void LoadTodos()
    {
        Task.Run(async () =>
        {
            Todos.Clear();

            IQueryable<TodoEntity> q = TodoMauiDbContext.Todos
                .OrderBy(v => v.IsDone)
                .ThenBy(v => v.TodoId);

            foreach (TodoEntity todoEntity in await q.ToListAsync())
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Todos.Add(new TodoItem()
                    {
                        TodoId = todoEntity.TodoId,
                        Note = todoEntity.Note,
                        IsDone = todoEntity.IsDone ? "✅" : "❌"
                    });
                });
            }
        }).Wait();
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public class TodoItem
{
    public long TodoId { get; set; }

    public string Note { get; set; }

    public string IsDone { get; set; }
}
