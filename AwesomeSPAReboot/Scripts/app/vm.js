define('vm', ['vm.shell', 'vm.images'],
    function (shell, images) {
        return {
            images: images,
            shell: shell
        };
    })