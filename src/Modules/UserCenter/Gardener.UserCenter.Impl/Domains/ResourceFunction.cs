// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    [Description("资源功能信息")]
    public class ResourceFunction : IEntity, IEntitySeedData<ResourceFunction>
    {
        /// <summary>
        /// 资源编号
        /// </summary>
        [Required]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [DisplayName("资源")]
        public Resource Resource { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<ResourceFunction> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new ResourceFunction[] 
            {







 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("c96dd7f7-f935-4499-8ef5-6d39fe26141a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("e2bb65e0-5d9e-485e-9059-8148fc236246"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("68ce42ff-acc7-485f-bc91-df471b520be7"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("713341f2-47e1-42af-b717-bfa75904d32e"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("03ee6f4b-dfea-4803-9515-3a9b2f907c90"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("38545a67-61ff-4e5c-90bb-a555a93fcbea"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),FunctionId = Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),FunctionId = Guid.Parse("e651d9a4-9d6d-44c7-a833-08da6ed19892"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),FunctionId = Guid.Parse("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),FunctionId = Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),FunctionId = Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),FunctionId = Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),FunctionId = Guid.Parse("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),FunctionId = Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),FunctionId = Guid.Parse("89954833-64a5-4c87-a717-9c863ca3b263"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),FunctionId = Guid.Parse("7a3399b3-6003-4aae-8e24-2e478992630e"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("97a7d440-b7fe-4af6-a8a1-18846c48828b"),FunctionId = Guid.Parse("5eb48cf2-6c45-47c2-a68b-84284a389c69"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),FunctionId = Guid.Parse("a8c06d41-806a-4bf5-8ceb-15995dac08cb"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("08ae2764-e551-45d2-9da7-49648481a8e0"),FunctionId = Guid.Parse("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("ba89c7b7-552c-415c-b4be-085262dc76b0"),FunctionId = Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),FunctionId = Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),FunctionId = Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),FunctionId = Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),FunctionId = Guid.Parse("05153ee4-dc99-4834-b398-5999f7dc8d01"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f077211f-0e79-44a3-935c-0f704f6a5962"),FunctionId = Guid.Parse("6dc1a088-15f6-43b8-8465-3a95cc495bab"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),FunctionId = Guid.Parse("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),FunctionId = Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("316ecba5-5d89-44ae-908f-a54268723bd1"),FunctionId = Guid.Parse("e23b555c-600a-4839-9439-2ee0ad0ae4f8"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b63d694e-205f-44c0-8353-0c9507f44696"),FunctionId = Guid.Parse("2502e6ae-879b-4674-a557-cd7b4de891a7"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),FunctionId = Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),FunctionId = Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId = Guid.Parse("0c6f2138-e984-4fba-ad2a-2890716a7259"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId = Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),FunctionId = Guid.Parse("6aea8a77-edd2-444b-b8be-901d78321a49"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),FunctionId = Guid.Parse("af79d7de-0141-4338-8c52-05216d1b07ff"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),FunctionId = Guid.Parse("0b605fe1-c77c-4735-8320-b8f400163ac9"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId = Guid.Parse("0d2e0194-2238-457b-aab0-9b3259cc4ed9"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId = Guid.Parse("3790cc0d-dc3a-4669-acba-3a90812c6386"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),FunctionId = Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),FunctionId = Guid.Parse("7120bd2f-4491-41ac-bef3-7cd86615da14"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),FunctionId = Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),FunctionId = Guid.Parse("622c1a11-7dff-4318-9d21-b57fbd1da9ba"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId = Guid.Parse("b56c4126-411c-445e-86aa-a91a5ce816d4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId = Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId = Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("2c1c895c-6434-4f14-91f2-144e48457101"),FunctionId = Guid.Parse("cba739f0-9f8a-40c2-afff-d66c3382e096"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),FunctionId = Guid.Parse("63d7208e-45d3-406e-a4a1-c87e3afda04d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),FunctionId = Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),FunctionId = Guid.Parse("01944b79-bfe5-4304-ade0-9c66e038d5d4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),FunctionId = Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),FunctionId = Guid.Parse("16517409-c055-447b-8e91-7155537c6d15"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId = Guid.Parse("cd7db809-50f5-4bf3-a464-89218e24077f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),FunctionId = Guid.Parse("868fc0df-7cdf-4b56-873e-16dd3e0aa528"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),FunctionId = Guid.Parse("2c3ec3c9-76c7-4d29-953f-e7430f22577b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),FunctionId = Guid.Parse("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),FunctionId = Guid.Parse("9ebd4172-5191-4931-9b22-4c339be4a816"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("de62a886-64b2-4a40-b70a-47eb08f23202"),FunctionId = Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),FunctionId = Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("6e487179-5bb2-4ab5-80e3-58c514c9595f"),FunctionId = Guid.Parse("8ae9c253-584e-46e4-b805-6ec90281d6dd"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),FunctionId = Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("a15ce231-80ae-46c6-ada8-49666e81e328"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),FunctionId = Guid.Parse("4c1b9201-09e6-421f-95d1-d98d009a3417"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("a53a9c89-7968-4598-9c46-dad4e9188bd0"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a1260e4c-e67c-4d72-a758-560a13e9c496"),FunctionId = Guid.Parse("af1f0410-e9cc-4a73-9da7-ea45aadac8b2"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),FunctionId = Guid.Parse("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("8d94c826-ddba-47fe-94c9-333880fee187"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),FunctionId = Guid.Parse("8172d258-7a75-4ced-b5e2-b0be7350aa1f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),FunctionId = Guid.Parse("5d67bd9d-853c-4e16-973d-be0511241fc0"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("92ed8299-ff26-4fae-b852-fe33f0c01a09"),FunctionId = Guid.Parse("cecdfb7d-6796-4bd8-a3d7-164c16a7c959"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("84256e5b-2cef-4b16-8fd3-79ff8d47c731"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),FunctionId = Guid.Parse("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),FunctionId = Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),FunctionId = Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("c591c0ca-3305-4684-89bb-278218d13c47"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),FunctionId = Guid.Parse("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),FunctionId = Guid.Parse("2f820c7f-4f1c-4737-aae6-329585c75d92"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),FunctionId = Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("45dd0581-3394-4c0a-bb8e-c9e0074d5611"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),FunctionId = Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),FunctionId = Guid.Parse("4b57474a-88b4-4393-bb49-4b59e8c3c41d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),FunctionId = Guid.Parse("571200a8-bde2-430b-84ea-743db7b282cd"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("0cbb3d40-de41-483e-a76c-3d85682176af"),FunctionId = Guid.Parse("f59833a1-c9af-4bb2-be4b-d6935513fc99"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),FunctionId = Guid.Parse("e651d9a4-9d6d-44c7-a833-08da6ed19892"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),FunctionId = Guid.Parse("10190ac3-1092-49a9-8ad2-313454b40447"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f02f906a-7579-478a-9406-3c8fd2c54886"),FunctionId = Guid.Parse("070ae0e4-0193-4ce0-8ba6-b8c344086ced"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a1958e51-06d4-4b29-9533-eae9d86c41d1"),FunctionId = Guid.Parse("cdd3c605-ed1d-4d94-a482-16430b729541"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),FunctionId = Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),FunctionId = Guid.Parse("4963631e-6343-469a-a189-10bfce6e3195"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("106a3a28-3143-4369-9215-cb223d1b0e45"),FunctionId = Guid.Parse("99546746-70b8-42d6-884d-ea1b79f88c0a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("1f8605fb-70b3-4929-89eb-4cda69cc305b"),FunctionId = Guid.Parse("26d95428-ebbd-4bf2-9bcc-2eeec4263bd5"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("145ec764-6a72-4c4f-85d3-7ad889193970"),FunctionId = Guid.Parse("31896c5d-2ed7-4e43-a952-4edc076d29d0"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("083fffc4-2600-49bb-87e6-1a92133499ec"),FunctionId = Guid.Parse("9191206c-f35e-4eb7-b19a-5949dc560369"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),FunctionId = Guid.Parse("2bf3ff67-c1a3-4426-8320-11839daa0a81"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("08baa5af-4718-4158-9276-1ad1068b9159"),FunctionId = Guid.Parse("33c2157a-884d-4030-abea-a9aeea51fdf8"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("7aad6dba-3f13-4982-adfa-525fa94485dd"),FunctionId = Guid.Parse("3ac59980-d2df-4363-b8db-a4d043e362e7"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),FunctionId = Guid.Parse("841c572c-5098-4e72-a590-2b81706aaa93"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),FunctionId = Guid.Parse("736fd9b6-b56a-4860-8a1c-9a077be886e3"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("af9b9a49-0094-4e1c-97dc-d0580525244f"),FunctionId = Guid.Parse("8c71dc07-b119-4462-a518-23189ec44356"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),FunctionId = Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("4f943ed1-997a-485f-9b54-9824b4ac285c"),FunctionId = Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId = Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId = Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),FunctionId = Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),FunctionId = Guid.Parse("1ef3b8a8-6e46-49d7-9a7e-f63137beaade"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("d697fda5-28fa-46c3-ba88-a98dd510e09d"),FunctionId = Guid.Parse("9fe5cc45-a851-4d3f-8b44-32dd96130946"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),FunctionId = Guid.Parse("38c69230-1ed0-413e-9ae6-05bc1ef989e0"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),FunctionId = Guid.Parse("9d25bf25-5470-4fed-b58c-c4ef4339d533"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a02edffb-0a63-4106-bac2-ea66f1f65060"),FunctionId = Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),FunctionId = Guid.Parse("39421a19-9cbf-477b-baea-34f40341357f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("86a086a1-0770-4df4-ade3-433ff7226399"),FunctionId = Guid.Parse("5c0a6241-ac2d-442f-9c6c-028566f18b6a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),FunctionId = Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),FunctionId = Guid.Parse("1d994e50-d40a-465b-8445-646041a8131a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),FunctionId = Guid.Parse("080dd200-8e8a-489c-86ca-8eb74c417c0b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),FunctionId = Guid.Parse("6e8d08f8-ba2a-4697-8b69-ac5a5bb31bff"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),FunctionId = Guid.Parse("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),FunctionId = Guid.Parse("7e5577d4-32b2-4f43-a83f-05410b59b195"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),FunctionId = Guid.Parse("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),FunctionId = Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)},
 new ResourceFunction(){ResourceId = Guid.Parse("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),FunctionId = Guid.Parse("8c71dc07-b119-4462-a518-23189ec44356"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1636443705)}
            };
        }
    }
}
