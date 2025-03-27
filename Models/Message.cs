namespace TFMessage.models;
using System.ComponentModel.DataAnnotations;

public class Message {
  public Message(int id, int name, int content)
  {
    Id = id;
    Name = name;
    Content = content;
  }

  public int Id {get;}
  public int Name {get;}
  public int Content {get;}
}

public class MessageDTO {
  public MessageDTO(string name, string content)
  {
    Name = name;
    Content = content;
  }

  [Required]
  public string Name {get;set;}
  [Required]
  public string Content {get;set;}
}
