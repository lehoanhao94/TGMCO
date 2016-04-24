var product = {
    init: function () {
        product.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ManagingProducts/ChangeStatusActive",
                date: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                }
            });
        })

        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ManagingProducts/ChangeStatusActive",
                date: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                }
            });
        })

        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/ManagingProducts/ChangeStatusActive",
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