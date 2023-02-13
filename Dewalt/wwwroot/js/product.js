getSubCategories($('select[name="categoryId"]').val());

function getSubCategories(id) {
    var o = { id: id };    
    $.post('/dashboard/product/subCategories', o, (d) => {
        $('select[name="subCategoryId"]').html(null);
        for (var i in d) {
            $('select[name="subCategoryId"]').append(`<option value="${d[i]['subCategoryId']}">${d[i]['subCategoryName']}</option>`);
        }        

        if (subCategoryId != null)
            $('select[name="subCategoryId"]').val(subCategoryId);        
    });
}    
$('select[name="categoryId"]').change(function () {
    subCategoryId = null;
    getSubCategories($(this).val());
})