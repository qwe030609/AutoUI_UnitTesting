using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    /// <summary>
    /// 定義 Test Item Variable Data type
    /// </summary>
    public enum VariableDataType : int
    {
        [System.ComponentModel.Description("Float")]
        Float = 1,

        [System.ComponentModel.Description("Integer")]
        Integer = 2,

        [System.ComponentModel.Description("Float(%)")]
        FloatPercentage = 3,

        [System.ComponentModel.Description("Short")]
        Short = 4,

        [System.ComponentModel.Description("String")]
        String = 5,

        [System.ComponentModel.Description("Byte")]
        Byte = 6,

        [System.ComponentModel.Description("Float[L]")]
        FloatArrayOfSize640 = 7,

        [System.ComponentModel.Description("Integer[L]")]
        IntegerArrayOfSize640 = 8,

        [System.ComponentModel.Description("Float(%)[L]")]
        FloatPercentageArrayOfSize640 = 9,

        [System.ComponentModel.Description("HexString")]
        HexString = 10,

        [System.ComponentModel.Description("Float[]")]
        FloatArray = 11,

        [System.ComponentModel.Description("Integer[]")]
        IntegerArray = 12,

        [System.ComponentModel.Description("LineInVector")]
        LineInVector = 13,

        [System.ComponentModel.Description("LoadVector")]
        LoadVector = 14,

        [System.ComponentModel.Description("SpecVector")]
        SpecVector = 15,

        [System.ComponentModel.Description("ExtMeasVector")]
        ExtMeasVector = 16,

        [System.ComponentModel.Description("ACLoadVector")]
        ACLoadVector = 17,

        [System.ComponentModel.Description("ACSpecVector")]
        ACSpecVector = 18,

        [System.ComponentModel.Description("ConstantVector")]
        ConstantVector = 19,

        [System.ComponentModel.Description("Double")]
        Double = 20,

        [System.ComponentModel.Description("Double[]")]
        DoubleArray = 21,

        [System.ComponentModel.Description("Double[L]")]
        DoubleArrayOfUUTMaxSize = 22,

        [System.ComponentModel.Description("String[]")]
        StringArray = 23,

        [System.ComponentModel.Description("Byte[]")]
        ByteArray = 24,

        [System.ComponentModel.Description("Double[,]")]
        DoubleMatrix = 25,

        [System.ComponentModel.Description("Float[,]")]
        FloatMatrix = 26,

        [System.ComponentModel.Description("Integer[,]")]
        IntegerMatrix = 27,

        [System.ComponentModel.Description("Byte[,]")]
        ByteMatrix = 28,

        [System.ComponentModel.Description("String[,]")]
        StringMatrix = 29,

        [System.ComponentModel.Description("String[L]")]
        StringArrayOfUUTMaxSize = 30,

        [System.ComponentModel.Description("Long")]
        Long = 31,

        [System.ComponentModel.Description("Long[]")]
        LongArray = 32,

        [System.ComponentModel.Description("HexString[]")]
        HexStringArray = 33,

        [System.ComponentModel.Description("HexString[,]")]
        HexStringMatrix = 34,

        [System.ComponentModel.Description("Long[,]")]
        LongMatrix = 35,

        [System.ComponentModel.Description("Chart")]
        Chart = 36,

        [System.ComponentModel.Description("Picture")]
        Picture = 37,
    }

    /// <summary>
    /// 定義 Test Item Variable Edit type
    /// </summary>
    public enum VariableEditType
    {
        EditBox = 1,
        ComboBox = 2,
        External_Signal = 3,
    }
}
