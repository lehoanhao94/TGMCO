var category = {
    init: function () {
        category.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ManagingCategories/ChangeStatus",
                date: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                }
            });
        })
    }
}

category.init();