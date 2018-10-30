<?php

use Symfony\Component\DependencyInjection\Argument\RewindableGenerator;

// This file has been auto-generated by the Symfony Dependency Injection Component for internal use.
// Returns the private 'console.command.cache_pool_prune' shared service.

$this->services['console.command.cache_pool_prune'] = $instance = new \Symfony\Bundle\FrameworkBundle\Command\CachePoolPruneCommand(new RewindableGenerator(function () {
    yield 'cache.app' => ${($_ = isset($this->services['cache.app']) ? $this->services['cache.app'] : $this->load('getCache_AppService.php')) && false ?: '_'};
}, 1));

$instance->setName('cache:pool:prune');

return $instance;
