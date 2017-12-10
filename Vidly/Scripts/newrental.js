$(document).ready(function () {

    window.removeMovie = function (countId, movieId) {
        $("#" + countId).remove();
        //remove the movie from the viewmodel object as well
        var index = vm.movieIds.indexOf(movieId);
        if (index > -1) {
            vm.movieIds.splice(index, 1);
        }
    };

    var vm = {
        movieIds: [],
    };

    var counterId = 0;

    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 3,
        highlight: true
    }, {
            name: 'customers',
            display: 'name',
            source: customers
        }).on("typeahead:select", function (e, customer) {
            vm.custId = customer.id;
        });

    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#movie').typeahead({
        minLength: 3,
        highlight: true
    }, {
            name: 'movies',
            display: 'name',
            source: movies
        }).on("typeahead:select", function (e, movie) {
            $('#moviesList').append("<li id='" + counterId + "' class='list-group-item'>" + movie.name +
                "<a href='javascript:removeMovie(" + counterId + ", " + movie.id + ")'><span class='glyphicon glyphicon-remove' style='float:right'></span></a></li>");
            counterId += 1;
            $('#movie').typeahead("val", "");
            vm.movieIds.push(movie.id);
        });

    $.validator.addMethod("validCustomer", function () {
        return vm.custId && vm.custId !== 0;
    }, "Please select a valid customer.");

    $.validator.addMethod("validMovies", function () {
        return vm.movieIds.length > 0;
    }, "Please select at least one movie from the movies list.");

    $.validator.setDefaults({
        ignore: ':hidden, .tt-hint'
    });

    $("#newRental").validate({
        submitHandler: function () {
            $.ajax({
                url: "/api/rentals/",
                method: "post",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals recorded successfully!");

                    $("#customer").typeahead("val", "");
                    $("#movie").typeahead("val", "");
                    $("#moviesList").empty();
                    vm = { movieIds: [] };
                    validator.resetForm();

                })
                .fail(function () {
                    toastr.error("Your rentals were not recorded.");
                });
            return false;
        }
    });
});