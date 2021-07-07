// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    [Description("资源功能信息")]
    public class ResourceFunction : IEntity, IEntitySeedData<ResourceFunction>
    {
        /// <summary>
        /// 权限Id
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


 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("bdecc6f3-86f4-4818-af34-5e61001bdeeb"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("39812ff6-2c40-4017-9d11-fd7b13fe2a6b"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("7602df12-8a81-4ab1-8314-df9ce948a876"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("03f2bbda-d0d4-429f-9c95-03345d00c2cd"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("fe3c8d2c-02ce-4073-a2b5-0b05168e7fc9"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("3204c8c0-2c00-47ea-b2b3-711c0e7a2c70"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId = Guid.Parse("2eb943e9-5b65-4572-b76e-4ef2de07bcd3"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),FunctionId = Guid.Parse("590cd04c-025c-4cc1-bdd1-e9cea201bb46"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),FunctionId = Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),FunctionId = Guid.Parse("7120bd2f-4491-41ac-bef3-7cd86615da14"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),FunctionId = Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),FunctionId = Guid.Parse("aedc9e9c-f011-4d46-966e-3b14fd5298c2"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),FunctionId = Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),FunctionId = Guid.Parse("9d9233d8-df0a-43b7-929a-65b9bd532c8c"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),FunctionId = Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),FunctionId = Guid.Parse("99264e5a-76d3-4f92-a56a-9c8711067218"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),FunctionId = Guid.Parse("6aea8a77-edd2-444b-b8be-901d78321a49"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("a1958e51-06d4-4b29-9533-eae9d86c41d1"),FunctionId = Guid.Parse("cdd3c605-ed1d-4d94-a482-16430b729541"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId = Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("f02f906a-7579-478a-9406-3c8fd2c54886"),FunctionId = Guid.Parse("070ae0e4-0193-4ce0-8ba6-b8c344086ced"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),FunctionId = Guid.Parse("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("a53a9c89-7968-4598-9c46-dad4e9188bd0"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("8d94c826-ddba-47fe-94c9-333880fee187"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("c591c0ca-3305-4684-89bb-278218d13c47"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("a15ce231-80ae-46c6-ada8-49666e81e328"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId = Guid.Parse("84256e5b-2cef-4b16-8fd3-79ff8d47c731"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),FunctionId = Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId = Guid.Parse("45dd0581-3394-4c0a-bb8e-c9e0074d5611"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),FunctionId = Guid.Parse("590cd04c-025c-4cc1-bdd1-e9cea201bb46"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),FunctionId = Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),FunctionId = Guid.Parse("a8c06d41-806a-4bf5-8ceb-15995dac08cb"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("08ae2764-e551-45d2-9da7-49648481a8e0"),FunctionId = Guid.Parse("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("97a7d440-b7fe-4af6-a8a1-18846c48828b"),FunctionId = Guid.Parse("5eb48cf2-6c45-47c2-a68b-84284a389c69"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("6e487179-5bb2-4ab5-80e3-58c514c9595f"),FunctionId = Guid.Parse("8ae9c253-584e-46e4-b805-6ec90281d6dd"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),FunctionId = Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),FunctionId = Guid.Parse("4b57474a-88b4-4393-bb49-4b59e8c3c41d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),FunctionId = Guid.Parse("10190ac3-1092-49a9-8ad2-313454b40447"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId = Guid.Parse("0d2e0194-2238-457b-aab0-9b3259cc4ed9"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId = Guid.Parse("3790cc0d-dc3a-4669-acba-3a90812c6386"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId = Guid.Parse("8cd2a3d1-3d4f-490d-8d64-3b2d93af79ed"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),FunctionId = Guid.Parse("af79d7de-0141-4338-8c52-05216d1b07ff"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),FunctionId = Guid.Parse("0b605fe1-c77c-4735-8320-b8f400163ac9"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId = Guid.Parse("0c6f2138-e984-4fba-ad2a-2890716a7259"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId = Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),FunctionId = Guid.Parse("9ebd4172-5191-4931-9b22-4c339be4a816"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),FunctionId = Guid.Parse("622c1a11-7dff-4318-9d21-b57fbd1da9ba"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),FunctionId = Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),FunctionId = Guid.Parse("7120bd2f-4491-41ac-bef3-7cd86615da14"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),FunctionId = Guid.Parse("868fc0df-7cdf-4b56-873e-16dd3e0aa528"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),FunctionId = Guid.Parse("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),FunctionId = Guid.Parse("99264e5a-76d3-4f92-a56a-9c8711067218"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),FunctionId = Guid.Parse("aedc9e9c-f011-4d46-966e-3b14fd5298c2"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),FunctionId = Guid.Parse("9d9233d8-df0a-43b7-929a-65b9bd532c8c"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId = Guid.Parse("38c69230-1ed0-413e-9ae6-05bc1ef989e0"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),FunctionId = Guid.Parse("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),FunctionId = Guid.Parse("03c9956e-b832-4202-9c47-55ba3793f606"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),FunctionId = Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),FunctionId = Guid.Parse("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),FunctionId = Guid.Parse("63d7208e-45d3-406e-a4a1-c87e3afda04d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),FunctionId = Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),FunctionId = Guid.Parse("2c3ec3c9-76c7-4d29-953f-e7430f22577b"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),FunctionId = Guid.Parse("01944b79-bfe5-4304-ade0-9c66e038d5d4"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),FunctionId = Guid.Parse("16517409-c055-447b-8e91-7155537c6d15"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),FunctionId = Guid.Parse("7e5577d4-32b2-4f43-a83f-05410b59b195"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId = Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),FunctionId = Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("46a9084a-0ae2-496e-bda5-e7e02a419a53"),FunctionId = Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),FunctionId = Guid.Parse("1d994e50-d40a-465b-8445-646041a8131a"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId = Guid.Parse("0838ba71-2502-43bc-be32-d2d77e71207d"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),FunctionId = Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),FunctionId = Guid.Parse("03c9956e-b832-4202-9c47-55ba3793f606"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),FunctionId = Guid.Parse("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),FunctionId = Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),FunctionId = Guid.Parse("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),FunctionId = Guid.Parse("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),FunctionId = Guid.Parse("080dd200-8e8a-489c-86ca-8eb74c417c0b"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("4f943ed1-997a-485f-9b54-9824b4ac285c"),FunctionId = Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),FunctionId = Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),FunctionId = Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),FunctionId = Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),FunctionId = Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),FunctionId = Guid.Parse("2502e6ae-879b-4674-a557-cd7b4de891a7"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("316ecba5-5d89-44ae-908f-a54268723bd1"),FunctionId = Guid.Parse("e23b555c-600a-4839-9439-2ee0ad0ae4f8"),CreatedTime= DateTimeOffset.Now},
 new ResourceFunction(){ResourceId = Guid.Parse("de62a886-64b2-4a40-b70a-47eb08f23202"),FunctionId = Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime= DateTimeOffset.Now}
            };
        }
    }
}
