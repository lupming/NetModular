using System;
using System.Collections.Generic;
using System.Text;

namespace Zyck.Frame.Extensions
{
    public class AliPayConfig
    {
        // 应用ID,您的APPID
        public static string AppId = "2016032401239609";

        // 支付宝网关
        public static string Gatewayurl = "https://openapi.alipay.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥
        public static string PrivateKey = "MIIEowIBAAKCAQEA5NDZ09UIXFGBl0QFt/VSBOLmg781EbONDpWeKeHohhhosxWpKGUaZbz0kMQ8/y3u8YjgfcilaRZtyK9VvcaAHanl9QIBA5Q0a6sNuGsWFwYp4QTTgth28PEavsV+nIdp3e1loMXJbij8x6Aac6V5ngXadPpreVelCy+cTooJ/HIoEgiI/5icKm+oFvx3wVuPX988DWqlv7uFuFV4SEbf8XFD5Nfv77/MQ8hW3U+lLJh0Is9BkhkCjAPFty/Ub+kysdP53SdyohClLRYOHPBl10+LyoFppsNFuDPpUeB2/g/z4UPF0KDBqD6xuxAh59oS5RjizQP3n55dFJNqPpqzCQIDAQABAoIBAHr7JZiNL2kvBY/fp5Mv711g9rRo2IUjE/V6xP+NxAQOqndWdQyZCo8T9/62+nHMplh7UO1KIl1RCMCMPHE7dzB6WHZPFWxvA5262iz8oWrOjUO77FKvNX5amB1g2KQYw/Xb2nuG0rS6ouJIUu9FSRB95ORqk8ksKjDQ7hnoZ2WekXAe+4SUtYWfa//VC3BMjznskIvo9BhhWjQvgeYgXI6TE+5bsiXLTNF8zjsCrhV6jty9kTmZLlp2xcfnQKEZo2kw+SzfBj4Lorgn43QXKsDpnP0BfqYHN4xGk8ONU8oIjEMl7+mXHdnDbZJpPR+FR+XS1nTqNR/1HMGRKKxSNGECgYEA8mILFxLoyPPjv0sPPGZPvZ4ReStxkL+f+pMUCeRPMaf+Ne1QViEFx4OhOIJoacWfRsLOUFMrQJ5Y0g8GIHMDRnV1RxiwMkV8RmYzAEdnEUjOXz3rIZuRPSjh4Wq6ciDmDhb8mr4fbmrZv1nU6lDREWbtdsGSxMAv8I6NMj9lsJsCgYEA8auvN6PkI0IF0lvv8NYw+cUXHmUhfaWKZuH4//7mZKDl3f069zsuKupLSPmCLEi8GK52owz3lXs06+AeAzus2j/TmuL9MDgrPl05QK37EJZ/GJ3O4qWx2PliqMRQPClqKDhEWu0+O6+ThgS8WrPMT0O3g3pQggvzta0Nh0NUKysCgYEA7xDE331HHO1xNLxTyr8e52eA1IKFAMAAiqWGnjsR4WhQ09+93TW0/ZRW7+WwFqNU9fYuiAuAHbwQ3x7ybA8Eq/ouMnGnV+4xJT6oDx5eCi5FXqszv3IxTqGEKVGbk/Xe8wa5H+dmGQJKwvFXj8UmcIOUtA05NdZZcmsn2Oeu5QECgYBIWYGnQqV1+LoNdACtNTKMU/jL3LLeDsUUb+o/mWwYDCNFBh61DT6REntfaXMTFuNY8EIMJkGouq3pP7s8dI0W+ay73eC2otovXXFab7uOB8Ac+oGJUNmhf2eN5teuayegyteL8SKEc9fSc3CAJvuvbzJtZhzcEf/wD9LQ/9Me6QKBgD2nroxp249Fm0Cl0ASMR4Zvlt9QLVpmtwnGNFZkhzzSdnclYZPMnT2lDK8WUaziENoKKwe8pwFdBdmLXPyzxCumaYqW6KnK6rynn7aJnCThjcL2mubDaP6nsg+XPebAWDjorLau+SuNyJfoALlzFA3oZUJ4QDv5dX1Ih097x0pB";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2RE1UBLs4HON5i2msPEzlyn2i1IloVALMudUpQrLybsaoceANojY4RY4nLt5s/37xPQXbTPXHdnPsX9CHhk1EgBKsX30GUFL8KU5+fnCXFU2cysaw4FyXArJ0iPDPEnXio9e0KzlaFMIeU0D4YmnUjD04WvkowmQmsdUK9FQYrfF2gqxLJ1fUOmu0asJTZVLIRyuYjdhlrpewuMVqXEcMWa+gWb8DzDf+caQKPJ+nxUXB35lVSj97RKOIyzx9/wcT+D9Babsgw7NG5id2FMHAjMGBYtHuNB3J2i6/coOcS6vpqz8wmCXzTBWb9zzp6jYWnLpgpBLSuA3Znt1ifKTfwIDAQAB";

        // 签名方式
        public static string SignType = "RSA2";

        // 编码格式
        public static string CharSet = "UTF-8";
    }

}
