<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="typehead.aspx.vb" Inherits="SolucionCalendario.typehead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>hola</title>
    <script src="bootstrap/js/jquery-3.2.1.min.js"></script>
    <script src="typehead/bootstrap3-typeahead.min.js"></script>
    <link href="typehead/scroll.css" rel="stylesheet" />
    <script type="text/javascript">
        var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
  'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii',
  'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana',
  'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota',
  'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
  'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
  'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island',
  'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
  'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
        ];
        var substringMatcher = function (strs) {
            return function findMatches(q, cb) {
                var matches, substringRegex;
                matches = [];
                substrRegex = new RegExp(q, 'i');


                $.each(strs, function (i, str) {
                    if (substrRegex.test(str)) {
                        matches.push(str);
                    }
                });

                cb(matches);
            };
        };

        $('#the-basics .typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 10
        },
        {
            name: 'states',
            source: substringMatcher(states)
        });


    </script>
</head>
<body>

    <input type='text' name="to" id="txtSearch" class="form-control" autocomplete="off" width="300" />
    <div id="the-basics">
        <input class="typeahead" type="text" placeholder="States of USA" />
    </div>

    <table>
        <thead>
            <tr>
                <th>Estado</th>
                <th>Participante</th>
                <th>Asistencia</th>

            </tr>
        </thead>
        <tbody>
            <tr>
                <td data-head='Nombre'>Fulano</td>
                <td data-head='Apellidos'>Mengano Zutano</td>
                <td data-head='Cargo'>Vende Humo</td>

            </tr>
            <tr>
                <td data-head='Nombre'>Mengano</td>
                <td data-head='Apellidos'>Fulano Zutano</td>
                <td data-head='Cargo'>Café tester</td>

            </tr>
            <tr>
                <td data-head='Nombre'>Zutano</td>
                <td data-head='Apellidos'>Fulano Mengano</td>
                <td data-head='Cargo'>Soñador</td>

            </tr>
            <tr>
                <td data-head='Nombre'>Fulanito</td>
                <td data-head='Apellidos'>Menganito Zutano</td>
                <td data-head='Cargo'>Pilla pilla</td>

            </tr>
        </tbody>
    </table>
</body>
</html>
