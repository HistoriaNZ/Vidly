$(document).ready(function () {
    var moviesTable = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: function (data, type, movie) {
                    return "<a href='/movies/details/" +
                        movie.id + "'>" + movie.name + "</a>";
                }
            },
            {
                data: "genre.name"
            }
        ]
    });
});