namespace Shared

type Todo =
    { Id: int
      Description: string
      IsCompleted: bool }

module Route =
    let builder typeName methodName = sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type TodosApi =
    { GetTodos: unit -> Async<Todo list>
      CreateTodo: string -> Async<unit>
      UpdateTodo: Todo -> Async<unit>
      DeleteTodo: int -> Async<unit> }