using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Pub
{

    public class PackageArea
    {
        /// <summary>
        /// 平面宽度
        /// </summary>
        public decimal width = 0;
        /// <summary>
        /// 平面高度
        /// </summary>
        public decimal height = 0;
        /// <summary>
        /// 平面长度
        /// </summary>
        public decimal length = 0;
        /// <summary>
        /// 平面开始起点X坐标
        /// </summary>
        public decimal beginx = 0;
        /// <summary>
        /// 左平面
        /// </summary>
        public PackageArea left;
        /// <summary>
        /// 右平面
        /// </summary>
        public PackageArea right;
        /// <summary>
        /// 平面内的卷烟集合
        /// </summary>
        public List<Cigarette> cigaretteList;
        /// <summary>
        /// 计算标志
        /// </summary>
        public decimal isscan = 0;
    }
    /// <summary>
    /// 卷烟
    /// </summary>
    public class Cigarette
    {
        /// <summary>
        /// 卷烟序号
        /// </summary>
        public decimal CigaretteNo;
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal width;
        /// <summary>
        /// 最小X坐标
        /// </summary>
        public decimal fromx;
        /// <summary>
        /// 最大X坐标
        /// </summary>
        public decimal tox;
        /// <summary>
        /// 平面条烟索引
        /// </summary>
        public decimal index;
    }
    /// <summary>
    /// 
    /// </summary>
    public class AreaUnit
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal width;
        /// <summary>
        /// 
        /// </summary>
        public decimal begin;
        /// <summary>
        /// 
        /// </summary>
        public decimal end;
        /// <summary>
        /// 开始坐标
        /// </summary>
        public decimal beginx;

    }
    /// <summary>
    /// 卷烟组
    /// </summary>
    public class ItemGroup
    {
        /// <summary>
        /// 卷烟编码
        /// </summary>
        public string CigaretteCode { get; set; }
        /// <summary>
        /// 条烟数
        /// </summary>
        public decimal Total { get; set; }
    }
    /// <summary>
    /// 卷烟组
    /// </summary>
    public class ItemGroup1
    {
        public decimal Cigindex { get; set; }
        /// <summary>
        /// 卷烟编码
        /// </summary>
        public string CigaretteCode { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public decimal Hight { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        /// 条烟数
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 条烟序号
        /// </summary>
        public decimal CigaretteSeq { get; set; }
        /// <summary>
        /// 摆放方向
        /// </summary>
        public decimal CDTYPE { get; set; }
        
        /// <summary>
        /// 双抓标志
        /// </summary>
        public string DoubleTake { get; set; }
    }
    /// <summary>
    /// 双抓卷烟组
    /// </summary>
    public class ItemGroups
    {
        /// <summary>
        /// 同高度卷烟序号（标记连续可双抓的烟，品牌可不同）
        /// </summary>
        public int CigaretteNo { get; set; }
        /// <summary>
        /// 卷烟组
        /// </summary>
        public List<ItemGroup1> Cigarette { get; set; }
    }

    public class ItemGroups2
    {
        /// <summary>
        /// 同高度卷烟序号（标记连续可双抓的烟，品牌可不同）
        /// </summary>
        public int CigaretteNo { get; set; }
        /// <summary>
        /// 卷烟组
        /// </summary>
        public List<ItemGroup1> Cigarette { get; set; }
    }
}

