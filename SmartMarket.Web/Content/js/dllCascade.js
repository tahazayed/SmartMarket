$(document).ready(function () {
    if ($("#ProductId").length > 0) {
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
    }


    if ($("#lstCategories").length > 0) {
        $('#lstCategories')
        .change(function () {
              categoryId =  $("#lstCategories").val();
              if (categoryId !== "-1") {
                  window.location.href = "/Products/Search?search=&companyId=&categoryId=" + categoryId;
              } else {
                  window.location.href = "/Products/Search?search=&companyId=&categoryId=";
              }

            });
    };

    if ($("#SearchBTN").length > 0) {
        $('#SearchBTN')
        .click(function (e) {
            e.preventDefault();
            search = $("#Search").val();

            if (search.length > 0) {
                window.location.href = "/Products/Search?companyId=&categoryId=&search=" + search;
            }

        });
    };

});