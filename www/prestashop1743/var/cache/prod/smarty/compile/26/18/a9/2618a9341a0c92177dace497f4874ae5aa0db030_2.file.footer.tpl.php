<?php
/* Smarty version 3.1.32, created on 2018-10-29 18:38:43
  from 'C:\wamp64\www\prestashop1743\themes\classic\templates\checkout\_partials\footer.tpl' */

/* @var Smarty_Internal_Template $_smarty_tpl */
if ($_smarty_tpl->_decodeProperties($_smarty_tpl, array (
  'version' => '3.1.32',
  'unifunc' => 'content_5bd753b39390f2_17359277',
  'has_nocache_code' => false,
  'file_dependency' => 
  array (
    '2618a9341a0c92177dace497f4874ae5aa0db030' => 
    array (
      0 => 'C:\\wamp64\\www\\prestashop1743\\themes\\classic\\templates\\checkout\\_partials\\footer.tpl',
      1 => 1537368256,
      2 => 'file',
    ),
  ),
  'includes' => 
  array (
  ),
),false)) {
function content_5bd753b39390f2_17359277 (Smarty_Internal_Template $_smarty_tpl) {
$_smarty_tpl->_loadInheritance();
$_smarty_tpl->inheritance->init($_smarty_tpl, false);
$_smarty_tpl->inheritance->instanceBlock($_smarty_tpl, 'Block_5592019755bd753b392f4a0_17830729', 'footer');
?>

<?php }
/* {block 'footer'} */
class Block_5592019755bd753b392f4a0_17830729 extends Smarty_Internal_Block
{
public $subBlocks = array (
  'footer' => 
  array (
    0 => 'Block_5592019755bd753b392f4a0_17830729',
  ),
);
public function callBlock(Smarty_Internal_Template $_smarty_tpl) {
?>

<div class="text-sm-center">
  <?php echo call_user_func_array( $_smarty_tpl->smarty->registered_plugins[Smarty::PLUGIN_FUNCTION]['l'][0], array( array('s'=>'%copyright% %year% - Ecommerce software by %prestashop%','sprintf'=>array('%prestashop%'=>'PrestaShop™','%year%'=>date('Y'),'%copyright%'=>'©'),'d'=>'Shop.Theme.Global'),$_smarty_tpl ) );?>

</div>
<?php
}
}
/* {/block 'footer'} */
}
