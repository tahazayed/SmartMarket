$(document).ready(function () {

    $('#ProductId')
            .change(function () {
                $.ajax({
                    type: "post",
                    url: "/Products/GetProdutPrice",
                    data: { ProductId: $('#ProductId').val() },
                    datatype: "json",
                    traditional: true,
                    success: function (data) {

                        $('#PricePerItem').html(data.PricePerItem);
                    }
                });
            });




    $('#ProductId').trigger('change');

});