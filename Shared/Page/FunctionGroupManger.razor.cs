using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Models.SystemInfo;
using Shared.Components.Sys;
using SqlSugar;

namespace Shared.Page
{
    public partial class FunctionGroupManger
    {
        [Inject]
        [NotNull]
        public ISqlSugarClient? db { get; set; }
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }
        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }
        public void OnClick(long id)
        {
            DialogService.ShowModal<EditMenu>(new ResultDialogOption()
            {
                ComponentParameters = new Dictionary<string, object>
                {
                    [nameof(EditMenu.Id)] = id,
                }
            });
        }
        private Task<FunctionGroup> OnAddAsync()
        {
            return Task.FromResult(new FunctionGroup() { FunctionPages = new List<FunctionPage>() });
        }

        private async Task<QueryData<FunctionGroup>> OnQueryAsync(QueryPageOptions options)
        {
            var item = await db.Queryable<FunctionGroup>().ToListAsync();
            return new QueryData<FunctionGroup>()
            {
                Items = item,
                TotalCount = item.Count,
            };
        }
        private async Task<bool> OnDeleteAsync(IEnumerable<FunctionGroup> items)
        {
            var res = await db.Deleteable<FunctionGroup>(items).ExecuteCommandAsync();
            return res > 0 ? true : false;
        }
        private async Task<bool> OnSaveAsync(FunctionGroup item, ItemChangedType changedType)
        {
            if (changedType == ItemChangedType.Add)
            {
                await db.Insertable(item).ExecuteReturnSnowflakeIdAsync();
            }
            else
            {
                await db.Updateable(item).ExecuteCommandAsync();
            }
            return true;
        }
    }
}