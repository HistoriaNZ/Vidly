$(document).ready(function () {

    var vm = {
        rentalIds: [],
    };

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
            $('#rentalsList').empty();
            vm.custId = customer.id;
            var rUrl = "/api/rentals/" + vm.custId;
            $.ajax({
                url: rUrl,
                method: "get",
                success: function (response) {
                    if (response.length == 0) {
                        $("#rentalsList").append("<li>There are no active rentals for this customer!</li>");
                    }
                    else {
                        $("#rentalsList").append("<div class='btn-group' data-toggle='buttons'>")
                        $.each(response, function (key, value) {
                            var rentalDate = new Date(value["dateRented"]).toDateString();
                            var movieName = value["movie"]["name"]
                            var rentalString = movieName + ", " + rentalDate;
                            var rentalId = value["id"];
                            $("#rentalsList").append("<div><label class='btn btn-primary btn-lg btn-block'><input type='checkbox' id='" + rentalId + "' >" + rentalString + "</label></div>");
                        })
                    };
                }
            });
        });

    $.validator.addMethod("validCustomer", function () {
        return vm.custId && vm.custId !== 0;
    }, "Please select a valid customer.");

    $("#rentalReturn").validate({
        submitHandler: function () {
            $('input:checked').each(function () {
                vm.rentalIds.push($(this).attr('id'));
            });

            $.ajax({
                url: "/api/rentals/",
                method: "put",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals updated successfully!");
                    $("#customer").typeahead("val", "");
                    $("#rentalsList").empty();
                    console.log("Data sent:" + vm.custId + ", " + vm.rentalIds);
                })
                .fail(function () {
                    toastr.error("Your returns were not recorded.");
                });
            return false;
        }
    });

});