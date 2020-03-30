module App

open Feliz

type Todo =
    { Id: int
      Description: string
      IsCompleted: bool }

let app =
    React.functionComponent (fun () ->
        let (todos, setTodos) = React.useState ([])
        let (todoInput, setTodoInput) = React.useState ("")

        React.fragment
            [ Html.form
                [ prop.onSubmit (fun e ->
                    e.preventDefault()
                    let todo =
                        { Id = todos.Length
                          Description = todoInput
                          IsCompleted = false }
                    setTodos (todo :: todos)
                    setTodoInput "")
                  prop.children
                      [ Html.input
                          [ prop.placeholder "What do you need to do?"
                            prop.value todoInput
                            prop.onChange setTodoInput ] ] ]
              Html.ul
                  [ prop.style
                      [ style.listStyleType.none
                        style.paddingLeft 0 ]
                    prop.children
                        (todos
                         |> List.map (fun i ->
                             Html.li
                                 [ prop.key i.Id
                                   prop.children
                                       [ Html.input
                                           [ prop.id (sprintf "todo%d" i.Id)
                                             prop.isChecked i.IsCompleted
                                             prop.onChange (fun isCompleted ->
                                                 setTodos
                                                     (todos
                                                      |> List.map (fun e ->
                                                          if e = i then { i with IsCompleted = isCompleted } else e)))
                                             prop.type'.checkbox ]
                                         Html.label
                                             [ prop.text i.Description
                                               prop.style [ i.IsCompleted, [ style.textDecorationLine.lineThrough ] ]
                                               prop.htmlFor (sprintf "todo%d" i.Id) ]
                                         (if i.IsCompleted then
                                             Html.button
                                                 [ prop.text "Remove"
                                                   prop.onClick (fun _ ->
                                                       setTodos (todos |> List.filter (fun e -> e <> i))) ]
                                          else
                                              React.fragment []) ] ])) ] ])

open Browser.Dom

ReactDOM.render (app, document.getElementById "root")
