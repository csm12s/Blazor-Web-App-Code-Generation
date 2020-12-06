// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Gardener.Application.Interfaces;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class Role
    {
        ITable table;
        RoleDto[] roles;
        IEnumerable<RoleDto> selectedRows;

        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        string _name = string.Empty;
        bool drawerVisible = false;
        string drawerTitle = String.Empty;
        bool formIsLoading = false;
        bool tableIsLoading = false;
        RoleDto editModel = new RoleDto();

        List<ResourceDto> resourceDtos = new List<ResourceDto>();
        [Inject]
        public MessageService MessaheSvr { get; set; }
        [Inject]
        public IRoleService RoleService { get; set; }
        [Inject]
        ConfirmService ConfirmSvr { get; set; }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            MessaheSvr.Config(new MessageGlobalConfig()
            {
                Top = 24,
                Duration = 1,
                MaxCount = 3,
                Rtl = true,
            });
            var resourceResult = await ResourceService.GetTree();
            if (resourceResult==null)
            {
                MessageService.Error("资源节点未加载到数据");
            }
            tree.DataSource = resourceResult;
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            tableIsLoading = true;
            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult!=null)
            {
                var pagedList = pagedListResult;
                roles = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                MessaheSvr.Error("加载失败");
            }
            tableIsLoading = false;
        }
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <returns></returns>
        private async Task OnReLoadTable()
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private async Task onChange(QueryModel<RoleDto> queryModel)
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(int id)
        {
            if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await RoleService.FakeDelete(id);
                if (result)
                {
                    roles = roles.Remove(roles.FirstOrDefault(x => x.Id == id));
                    MessaheSvr.Success("删除成功");
                }
                else
                {
                    MessaheSvr.Error("删除失败");
                }
                //await InvokeAsync(StateHasChanged);
            }

        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(RoleDto roleDto)
        {
            drawerTitle = "编辑";
            roleDto.Adapt(editModel);
            drawerVisible = true;
            //await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            new RoleDto().Adapt(editModel);
            drawerTitle = "添加";
            drawerVisible = true;
            //await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 抽屉关闭时
        /// </summary>
        private async Task OnDrawerClose()
        {
            drawerVisible = false;
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            //开始请求
            if (editModel.Id == 0)
            {
                //添加
                var result = await RoleService.Insert(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result!=null)
                {
                    MessaheSvr.Success("添加成功");
                    _pageIndex = 1;
                    _name = string.Empty;
                    await ReLoadTable();
                }
                else
                {
                    MessaheSvr.Success("添加失败");
                }

            }
            else
            {
                //修改
                var result = await RoleService.Update(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result)
                {
                    await ReLoadTable();
                    MessaheSvr.Success("修改成功", 1);
                }
                else
                {
                    MessaheSvr.Success("修改失败", 1);
                }

            }

        }
        /// <summary>
        /// 表单失败时
        /// </summary>
        /// <param name="editContext"></param>
        private async Task OnFormFinishFailed(EditContext editContext)
        {
            //drawerVisible = false;
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (selectedRows == null || selectedRows.Count() == 0)
            {
                MessaheSvr.Warn("未选中任何行");
            }
            else
            {
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await RoleService.FakeDeletes(selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        roles = roles.Where(x => !selectedRows.Any(y => y.Id == x.Id)).ToArray();
                        MessaheSvr.Success("删除成功");
                    }
                    else
                    {
                        MessaheSvr.Error($"删除失败");
                    }
                    //await InvokeAsync(StateHasChanged);
                }
            }
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeIsLocked(RoleDto model, bool isLocked)
        {
            ChangeIsLocked(model, isLocked);
        }
        private async Task ChangeIsLocked(RoleDto model, bool isLocked)
        {
            var result = await RoleService.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                MessaheSvr.Error("锁定失败");
            }
        }

        #region 分配资源

        private int currentEditRoleResourceRoleId = 0;
        private bool editRoleResourceDrawerVisible;
        private bool editRoleResourceTreeIsLoading;
        private string editRoleResourceDrawerTitle = "分配权限";
        private Tree tree;
        private bool isExpanded;
        [Inject]
        IResourceService ResourceService { get; set; }
        [Inject]
        MessageService MessageService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(RoleDto role)
        {
            currentEditRoleResourceRoleId = role.Id;
            await LoadTreeData(role.Id);
        }
        /// <summary>
        /// 加载树数据
        /// </summary>
        /// <returns></returns>
        private async Task LoadTreeData(int roleId)
        {
            editRoleResourceTreeIsLoading = true;
            //选中已有资源
            var roleResourceResult = await RoleService.GetResource(roleId);
            //
            if (roleResourceResult==null)
            {
                MessageService.Error("已分配资源加载失败");
                editRoleResourceTreeIsLoading = false;
                return;
            }
            await Check(tree.ChildNodes, (id) =>
            {
                if (roleResourceResult == null) return false;
                return roleResourceResult.Any(x => x.Id == id && (x.Children == null || !x.Children.Any()));
            });
            editRoleResourceTreeIsLoading = false;
            editRoleResourceDrawerVisible = true;
            return;
        }
        /// <summary>
        /// 递归展开或关闭节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private async Task Check(List<TreeNode> nodes, Func<Guid, bool> flagFunc)
        {
            foreach (var node in nodes)
            {
                var flag = flagFunc(((ResourceDto)node.DataItem).Id);
                //有变化再进行变更
                if (node.IsChecked != flag)
                {
                    node.SetChecked(flag);
                }
                await Check(node.ChildNodes, flagFunc);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceDrawerClose()
        {
            editRoleResourceDrawerVisible = false;
            //tree.DecheckedAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task OnEditRoleResourceSave(MouseEventArgs e)
        {

            Guid[] resourceIds = new Guid[] { };

            if (tree.CheckedNodes?.Count > 0)
            {
                List<TreeNode> parents = new List<TreeNode>();
                tree.CheckedNodes.ForEach(x =>
                {
                    parents.AddRange(GetParents(x));
                });
                parents.AddRange(tree.CheckedNodes);
                resourceIds=parents.Select(x => ((ResourceDto)x.DataItem).Id).Distinct().ToArray();
            }
            //删除所有资源
            var result = await RoleService.Resource(currentEditRoleResourceRoleId, resourceIds);
            if (result)
            {
                MessageService.Success("保存成功");
                editRoleResourceDrawerVisible = false;
            }
            else
            {
                MessageService.Error("保存失败");
            }

        }
        /// <summary>
        /// 获取所有父级
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<TreeNode> GetParents(TreeNode node)
        {
            List<TreeNode> ids = new List<TreeNode>();
            if (node.ParentNode != null)
            {
                ids.Add(node.ParentNode);
                ids.AddRange(GetParents(node.ParentNode));
            }
            return ids;
        }

        /// <summary>
        /// 递归展开或关闭节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private async Task Expand(List<TreeNode> nodes, bool flag)
        {
            foreach (var node in nodes)
            {
                node.Expand(flag);
                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    await Expand(node.ChildNodes, flag);
                }
            }
        }
        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private async Task OnExpandClick()
        {
            isExpanded = !isExpanded;

            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                //仅操作选中的节点
                await Expand(new List<TreeNode> { selectedNode }, isExpanded);
            }
            else
            {
                //操作所有的节点
                await Expand(tree.ChildNodes, isExpanded);
            }
        }

        #endregion
    }
}
