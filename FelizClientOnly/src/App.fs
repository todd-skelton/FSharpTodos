module App

type Todo =
    { Id: int
      Description: string
      IsCompleted: bool }

open Feliz
open Feliz.MaterialUI
open Fable.MaterialUI.Icons

let useStyles =
    Styles.makeStyles (fun styles theme ->
        {| paper =
               styles.create
                   [ style.marginTop (theme.spacing 4)
                     style.padding (theme.spacing 2)
                     style.display.flex
                     style.flexDirection.column ]
           list =
               styles.create
                   [ style.listStyleType.none
                     style.paddingLeft 0 ] |})

let app =
    React.functionComponent (fun () ->
        let classes = useStyles()
        let (todos, setTodos) = React.useState ([])
        let (todoInput, setTodoInput) = React.useState ("")

        let handleSubmit (e: Browser.Types.Event) =
            e.preventDefault()
            let todo =
                { Id = todos.Length
                  Description = todoInput
                  IsCompleted = false }
            setTodos (todo :: todos)
            setTodoInput ""

        let handleListItemClick todo =
            (fun _ ->
                setTodos
                    (todos
                     |> List.map (fun e ->
                         if e = todo
                         then { todo with IsCompleted = not todo.IsCompleted }
                         else e)))

        let handleRemove todo = (fun _ -> setTodos (todos |> List.filter (fun e -> e <> todo)))

        React.fragment
            [ Mui.appBar
                [ appBar.position.static'
                  appBar.children
                      [ Mui.toolbar
                          [ Mui.typography
                              [ typography.variant.h6
                                prop.text "FSharp Todos" ] ] ] ]
              Mui.container
                  [ container.component' "main"
                    container.maxWidth.sm
                    container.children
                        [ Mui.paper
                            [ prop.className classes.paper
                              paper.component' "form"
                              prop.onSubmit handleSubmit
                              paper.children
                                  [ Mui.textField
                                      [ textField.label "What needs to get done?"
                                        textField.value todoInput
                                        textField.onChange setTodoInput
                                        textField.fullWidth true ]
                                    Mui.list
                                        [ prop.className classes.list
                                          prop.children
                                              (todos
                                               |> List.map (fun i ->
                                                   Mui.listItem
                                                       [ prop.key i.Id
                                                         prop.onClick (handleListItemClick i)
                                                         listItem.button true
                                                         prop.children
                                                             [ Mui.listItemIcon
                                                                 [ prop.children
                                                                     [ Mui.checkbox
                                                                         [ checkbox.edge.start
                                                                           checkbox.disableRipple true
                                                                           checkbox.checked' i.IsCompleted ] ] ]
                                                               Mui.listItemText
                                                                   [ prop.text i.Description
                                                                     prop.style
                                                                         [ i.IsCompleted,
                                                                           [ style.textDecorationLine.lineThrough ] ] ]
                                                               (if i.IsCompleted then
                                                                   Mui.listItemSecondaryAction
                                                                       [ Mui.iconButton
                                                                           [ iconButton.edge.end'
                                                                             iconButton.color.secondary
                                                                             prop.onClick (handleRemove i)
                                                                             prop.children [ deleteIcon [] ] ] ]
                                                                else
                                                                    React.fragment []) ] ])) ] ] ] ] ] ])

open Browser.Dom

ReactDOM.render (app, document.getElementById "root")
