$(document).ready(function () {
    $(".nav li").removeClass("active");
    $("#home").addClass("active");
});
DeleteProduct = function (product) {
    var id = product.dataset.id;
    $.ajax({
        method: "DELETE",
        url: "/product/deleteProduct?productId=" + id,
    })
  .done(function (data) {
      var isDeleted = data.IsDeleted;
      if (isDeleted) {
          alert('Successfully removed product');
          location.reload();
      } else {
          alert('Unable to remove product');
      }
  });
};
//li-search-product
$('#add-product').click(function () {
    $(".nav li").removeClass("active");
    $("#li-add-product").addClass("active");//product-list
    $('#product-list').html("");
    $.ajax({
        method: "GET",
        url: "/product/AddProduct",
        datatype: 'html'
    })
  .done(function (data) {
      $('#product-list').html(data);
  });
});
$('#search-product').click(function () {
    $(".nav li").removeClass("active");
    $("#li-search-product").addClass("active");//product-list
    $('#product-list').html("");
    $.ajax({
        method: "GET",
        url: "/product/SearchProduct",
        datatype: 'html'
    })
  .done(function (data) {
      $('#product-list').html(data);
  });
});

AddProduct = function () {
    if (ValidateAddProduct()) {
        var product = {
            Name: $('#inputName').val(),
            Category: $('#inputCategory').val(),
            Price: $('#inputPrice').val()
        };
        $.ajax({
            method: "POST",
            url: "/product/AddProduct",
            data: JSON.stringify(product),
            datatype: 'json',
            contentType: "application/json",
            success: function () {
                alert("Product added successfully");
                location.reload();
            },
            error: function () {
                alert("Unable to add product");
                location.reload();
            }
        });
    }

};
SearchProduct = function () {
    var isValid = ValidateSearchRequest();
    if (isValid) {
        var productId = $('#inputProductId').val();
        $('#product-list').html("");
        $.ajax({
            method: "GET",
            url: "/product/SearchResultProduct?productId=" + productId,
            success: function(data) {
                $('#product-list').html(data);
            },
            error: function() {
                alert("Searched product not found");
            }
        });
        
    }
};
ValidateSearchRequest = function () {
    if ($('#inputProductId').val() !== "" && $('#inputProductId').val() !== undefined && $('#inputProductId').val() !== null) {
        return true;
    }
    return false;
};
ValidateAddProduct = function() {

    if (
        $('#inputName').val() !== "" && $('#inputName').val() !== undefined && $('#inputName').val() !== null &&
            $('#inputCategory').val() !== "" && $('#inputCategory').val() !== undefined && $('#inputCategory').val() !== null &&
            $('#inputPrice').val() !== "" && $('#inputPrice').val() !== undefined && $('#inputPrice').val() !== null
    ) {
        return true;
    }
    return false;
}