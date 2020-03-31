namespace Shared

type Todo =
    { Id: int
      Description: string
      IsCompleted: bool }

module Route =
    open System.Text.RegularExpressions

    let toKebabCase input =
        match input with
        | null -> input
        | "" -> input
        | _ -> Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1-$2").ToLower()

    let builder typeName methodName = sprintf "/api/%s/%s" (typeName |> toKebabCase) (methodName |> toKebabCase)

type TodosApi =
    { GetTodos: unit -> Async<Todo list>
      CreateTodo: string -> Async<unit>
      UpdateTodo: Todo -> Async<unit>
      DeleteTodo: int -> Async<unit> }
