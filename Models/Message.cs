namespace TFMessage.models;
using System.ComponentModel.DataAnnotations;

public class Message {
  public Message(string id, string author, string content)
  {
    Id = id;
    Author = author;
    Content = content;
  }

  public string Id {get;}
  public string Author {get;}
  public string Content {get;}

  public override string ToString()
  {
    return $"{Id},{Author},{Content}";
  }
}

public class MessageDTO {
  public MessageDTO(string author, string content)
  {
    Author = author;
    Content = content;
  }

  [Required]
  public string Author {get;set;}
  [Required]
  public string Content {get;set;}

  public Message GetMessage(){
    string id = Guid.NewGuid().ToString(); 
    return new Message(id,Author,Content); 
  }
}
