using System.IO.Enumeration;
namespace Infrastructure.Services;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class TodoListImageService
{
    private readonly DataContext _context;

    public TodoListImageService(DataContext context)
    {
        _context = context;
    }

    public async  Task<List<GetTodoListImage>> Get()
    {
        var list  = await _context.TodoListImages.Select(t => new GetTodoListImage()
        {
         Id = t.Id,
        ImageName = t.ImageName,
        Title = t.TodoList.Title,
        Color = t.TodoList.Color
        }).ToListAsync();

        return list;
    }
    
    public async Task<AddTodoListImage> Add(AddTodoListImage todoListImage)
    {
        var newTodoListImageList =  new TodoListImage()
        {

            ImageName = todoListImage.ImageName ,
            TodoListId = todoListImage.TodoListId       
        };
        _context.TodoListImages.Add(newTodoListImageList);
        await _context.SaveChangesAsync();

        return todoListImage;
    }
    public async Task<AddTodoListImage> Update(AddTodoListImage todoListImage)
    {
        var find = await _context.TodoListImages.FindAsync(todoListImage.Id);
        if(find == null ) return new AddTodoListImage();
    
       find.ImageName = todoListImage.ImageName;
       find.TodoListId = todoListImage.TodoListId;

        await _context.SaveChangesAsync();

        return todoListImage;
        
    }
  public async Task<string> Delete(int id)
    {
        var find = await _context.TodoListImages.FindAsync(id);
         _context.TodoListImages.Remove(find);

        await _context.SaveChangesAsync();
        return "Deleted";

    }

}