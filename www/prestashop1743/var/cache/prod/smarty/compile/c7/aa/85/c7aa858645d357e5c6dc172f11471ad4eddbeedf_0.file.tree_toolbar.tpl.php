<?php
/* Smarty version 3.1.32, created on 2018-10-29 20:21:45
  from 'C:\wamp64\www\prestashop1743\admin753\themes\default\template\helpers\tree\tree_toolbar.tpl' */

/* @var Smarty_Internal_Template $_smarty_tpl */
if ($_smarty_tpl->_decodeProperties($_smarty_tpl, array (
  'version' => '3.1.32',
  'unifunc' => 'content_5bd75dc9d78741_15062736',
  'has_nocache_code' => false,
  'file_dependency' => 
  array (
    'c7aa858645d357e5c6dc172f11471ad4eddbeedf' => 
    array (
      0 => 'C:\\wamp64\\www\\prestashop1743\\admin753\\themes\\default\\template\\helpers\\tree\\tree_toolbar.tpl',
      1 => 1537368256,
      2 => 'file',
    ),
  ),
  'includes' => 
  array (
  ),
),false)) {
function content_5bd75dc9d78741_15062736 (Smarty_Internal_Template $_smarty_tpl) {
?><div class="tree-actions pull-right">
	<?php if (isset($_smarty_tpl->tpl_vars['actions']->value)) {?>
	<?php
$_from = $_smarty_tpl->smarty->ext->_foreach->init($_smarty_tpl, $_smarty_tpl->tpl_vars['actions']->value, 'action');
if ($_from !== null) {
foreach ($_from as $_smarty_tpl->tpl_vars['action']->value) {
?>
		<?php echo $_smarty_tpl->tpl_vars['action']->value->render();?>

	<?php
}
}
$_smarty_tpl->smarty->ext->_foreach->restore($_smarty_tpl, 1);?>
	<?php }?>
</div>
<?php }
}
