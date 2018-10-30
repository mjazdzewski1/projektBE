<?php
/* Smarty version 3.1.32, created on 2018-10-29 20:21:45
  from 'C:\wamp64\www\prestashop1743\admin753\themes\default\template\helpers\tree\tree_header.tpl' */

/* @var Smarty_Internal_Template $_smarty_tpl */
if ($_smarty_tpl->_decodeProperties($_smarty_tpl, array (
  'version' => '3.1.32',
  'unifunc' => 'content_5bd75dc9dc5457_71125493',
  'has_nocache_code' => false,
  'file_dependency' => 
  array (
    'ec2b0b07695eeb135928db059bf810deda7d5f38' => 
    array (
      0 => 'C:\\wamp64\\www\\prestashop1743\\admin753\\themes\\default\\template\\helpers\\tree\\tree_header.tpl',
      1 => 1537368256,
      2 => 'file',
    ),
  ),
  'includes' => 
  array (
  ),
),false)) {
function content_5bd75dc9dc5457_71125493 (Smarty_Internal_Template $_smarty_tpl) {
?><div class="tree-panel-heading-controls clearfix">
	<?php if (isset($_smarty_tpl->tpl_vars['title']->value)) {?><i class="icon-tag"></i>&nbsp;<?php echo call_user_func_array( $_smarty_tpl->smarty->registered_plugins[Smarty::PLUGIN_FUNCTION]['l'][0], array( array('s'=>$_smarty_tpl->tpl_vars['title']->value),$_smarty_tpl ) );
}?>
	<?php if (isset($_smarty_tpl->tpl_vars['toolbar']->value)) {
echo $_smarty_tpl->tpl_vars['toolbar']->value;
}?>
</div>
<?php }
}
