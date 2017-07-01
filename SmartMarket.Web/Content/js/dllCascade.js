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
            categoryId = $("#lstCategories").val();
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


    if ($(".RemoveFromCartLink").length > 0) {
        $(".RemoveFromCartLink")
               .click(function () {
                   // Get the id from the link
                   var recordToDelete = $(this).attr("data-id");
                   if (recordToDelete != '') {
                       // Perform the ajax post
                       $.post("/ShoppingCart/RemoveFromCart",
                           { "id": recordToDelete },
                           function (data) {
                               // Successful requests get here
                               // Update the page elements
                               $('#item-count-' + data.DeleteId).text(data.ItemCount);
                               if (data.ItemCount === 0) {

                                   if ($('#row-' + data.DeleteId).length > 0) {
                                       $('#row-' + data.DeleteId).fadeOut('slow');
                                   }

                               }
                               $('#cart-total').text(data.CartTotal);
                               $('#update-message').text(data.Message);
                               $('#cart-status').text('Cart (' + data.CartCount + ')');
                           });
                   }
               });
    };

    if ($(".AddToCartLink").length > 0) {
        $(".AddToCartLink")
               .click(function () {
                   // Get the id from the link
                   var recordToDelete = $(this).attr("data-id");
                   if (recordToDelete != '') {
                       // Perform the ajax post
                       $.post("/ShoppingCart/AddToCart",
                           { "id": recordToDelete },
                           function (data) {
                               // Successful requests get here
                               // Update the page elements
                               $('#item-count-' + data.DeleteId).text(data.ItemCount);
                               if (data.ItemCount === 0) {

                                   if ($('#row-' + data.DeleteId).length > 0) {
                                       $('#row-' + data.DeleteId).fadeOut('slow');
                                   }

                               }
                               $('#cart-total').text(data.CartTotal);
                               $('#update-message').text(data.Message);
                               $('#cart-status').text('Cart (' + data.CartCount + ')');
                           });
                   }
               });
    };


    if ($(".CheckoutLink").length > 0) {
        $(".CheckoutLink")
               .click(function () {

                   // Perform the ajax post
                   $.post("/ShoppingCart/AddOrder",
                       {},
                       function (data) {
                           // Successful requests get here
                           // Update the page elements
                           $('update-message').text(data.Message);


                       });

               });
    };
    var UpdateCount = function () {
        $.post("/ShoppingCart/GetCartItems",
                           {},
                           function (data) {

                               // Successful requests get here
                               // Update the page elements
                               $.each(data.lstItems, function (index) {
                                   console.log(data.lstItems[index]);
                                   $('#item-count-' + data.lstItems[index].ProductId).text(data.lstItems[index].Count);
                               });
                           });
    };
    UpdateCount();
});