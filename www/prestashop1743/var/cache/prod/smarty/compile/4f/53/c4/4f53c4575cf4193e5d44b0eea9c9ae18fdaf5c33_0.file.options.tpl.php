<?php
/* Smarty version 3.1.32, created on 2018-10-29 20:06:45
  from 'C:\wamp64\www\prestashop1743\admin753\themes\default\template\controllers\stores\helpers\options\options.tpl' */

/* @var Smarty_Internal_Template $_smarty_tpl */
if ($_smarty_tpl->_decodeProperties($_smarty_tpl, array (
  'version' => '3.1.32',
  'unifunc' => 'content_5bd75a45923182_41498698',
  'has_nocache_code' => false,
  'file_dependency' => 
  array (
    '4f53c4575cf4193e5d44b0eea9c9ae18fdaf5c33' => 
    array (
      0 => 'C:\\wamp64\\www\\prestashop1743\\admin753\\themes\\default\\template\\controllers\\stores\\helpers\\options\\options.tpl',
      1 => 1537368256,
      2 => 'file',
    ),
  ),
  'includes' => 
  array (
  ),
),false)) {
function content_5bd75a45923182_41498698 (Smarty_Internal_Template $_smarty_tpl) {
$_smarty_tpl->_loadInheritance();
$_smarty_tpl->inheritance->init($_smarty_tpl, true);
?>



<?php 
$_smarty_tpl->inheritance->instanceBlock($_smarty_tpl, 'Block_2570443005bd75a4590b395_50707864', "after");
?>

<?php $_smarty_tpl->inheritance->endChild($_smarty_tpl, "helpers/options/options.tpl");
}
/* {block "after"} */
class Block_2570443005bd75a4590b395_50707864 extends Smarty_Internal_Block
{
public $subBlocks = array (
  'after' => 
  array (
    0 => 'Block_2570443005bd75a4590b395_50707864',
  ),
);
public function callBlock(Smarty_Internal_Template $_smarty_tpl) {
?>

<?php echo '<script'; ?>
 type="text/javascript">
    function ajaxStoreStates(id_state_selected)
{
    $.ajax({
	url: "index.php",
	cache: false,
	data: "ajax=1&tab=AdminStates&token=<?php echo call_user_func_array( $_smarty_tpl->smarty->registered_plugins[Smarty::PLUGIN_FUNCTION]['getAdminToken'][0], array( array('tab'=>'AdminStates'),$_smarty_tpl ) );?>
&action=states&id_country="+$('#PS_SHOP_COUNTRY_ID').val() + "&id_state=" + $('#PS_SHOP_STATE_ID').val(),
	success: function(html)
	{
	    if (html == 'false')
	    {
		$("#conf_id_PS_SHOP_STATE_ID").parent().fadeOut();
		$('#PS_SHOP_STATE_ID option[value=0]').attr("selected", "selected");
	    }
	    else
	    {
		$("#PS_SHOP_STATE_ID").html(html);
		$("#conf_id_PS_SHOP_STATE_ID").parent().fadeIn();
		$('#PS_SHOP_STATE_ID option[value=' + id_state_selected + ']').attr("selected", "selected");
	    }
	}
    });
}

$(document).ready(function(){
    <?php if (isset($_smarty_tpl->tpl_vars['categoryData']->value['fields']['PS_SHOP_STATE_ID']['value'])) {?>
	    if ($('#PS_SHOP_COUNTRY_ID') && $('#PS_SHOP_STATE_ID'))
	    {
			ajaxStoreStates(<?php echo $_smarty_tpl->tpl_vars['categoryData']->value['fields']['PS_SHOP_STATE_ID']['value'];?>
);
			$('#PS_SHOP_COUNTRY_ID').change(function() {
			    ajaxStoreStates();
			});
	    }
    <?php }?>
});
<?php echo '</script'; ?>
>
<?php
}
}
/* {/block "after"} */
}
