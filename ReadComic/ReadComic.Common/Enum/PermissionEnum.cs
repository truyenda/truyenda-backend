using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Common.Enum
{
    public class PermissionEnum
    {
        public enum Permission
        {
            ACCOUNT_MAN = 1,
            ACCOUNT_LIS = 2,
            ACCOUNT_GET = 4,
            ACCOUNT_UPD = 8,
            ACCOUNT_DEL = 16,
            ACCOUNT_PER = 32,
            QuyenQuanLyChuong = 64,
            CHAPTER_MAN = 128,
            CHAPTER_CRE = 256,
            CHAPTER_UPD = 512,
            CHAPTER_DEL = 1024,
            QuyenQuanLyTruyen = 2048,
            STORY_MAN = 4096,
            STORY_CRE = 8192,
            STORY_UPD = 16384,
            STORY_DEL = 32768,
            QuyenQuanLyTheLoai = 65536,
            CATEGORY_MAN = 131072,
            CATEGORY_CRE = 262144,
            CATEGORY_UPD = 524288,
            CATEGORY_DEL = 1048576,
            QuyenQuanTriTacGia = 2097152,
            AUTHOR_MAN = 4194304,
            AUTHOR_CRE = 8388608,
            AUTHOR_UPD = 16777216,
            AUTHOR_DEL = 33554432,
            QuyenQuanLyChuKy = 67108864,
            FREQUENCY_MAN = 134217728,
            FREQUENCY_CRE = 268435456,
            FREQUENCY_UPD = 536870912,
            FREQUENCY_DEL = 1073741824,
            //QuyenQuanTriTrangThaiTruyen = 2147483648,
            //SSTATUS_MAN = 4294967296,
            //SSTATUS_CRE = 8589934592,
            //SSTATUS_UPD =17179869184,
            //SSTATUS_DEL =34359738368,
            //QuyenQuanTriTeam =68719476736,
            //TEAM_MAN =137438953472,
            //TEAM_UPD =274877906944,
            //TEAM_DEL =549755813888,
            //QuyenQuanTriTeamMember =1099511627776,
            //TEAMMEM_MAN =2199023255552,
            //TEAMMEM_LIS =4398046511104,
            //TEAMMEM_ADD =8796093022208,
            //TEAMMEM_GET =17592186044416,
            //TEAMMEM_PER =35184372088832,
            //TEAMMEM_DEL =70368744177664
        }
    }
}