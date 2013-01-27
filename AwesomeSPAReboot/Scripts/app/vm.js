define('vm', ['vm.shell', 'vm.images', 'vm.stats'],
    function(shell, images, stats) {
        return {
            images: images,
            shell: shell,
            stats: stats
        };
    });