<?php

use Symfony\Component\DependencyInjection\Argument\RewindableGenerator;

// This file has been auto-generated by the Symfony Dependency Injection Component for internal use.
// Returns the public 'prestashop.multishop_command_listener' shared service.

return $this->services['prestashop.multishop_command_listener'] = new \PrestaShopBundle\EventListener\MultishopCommandListener(${($_ = isset($this->services['prestashop.adapter.shop.context']) ? $this->services['prestashop.adapter.shop.context'] : $this->services['prestashop.adapter.shop.context'] = new \PrestaShop\PrestaShop\Adapter\Shop\Context()) && false ?: '_'}, ${($_ = isset($this->services['kernel']) ? $this->services['kernel'] : $this->get('kernel')) && false ?: '_'}->getRootDir());
