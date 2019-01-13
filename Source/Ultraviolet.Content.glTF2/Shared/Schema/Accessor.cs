//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace glTFLoader.Schema {
    using System.Linq;
    using System.Runtime.Serialization;
    
    
    public class Accessor {
        
        /// <summary>
        /// Backing field for BufferView.
        /// </summary>
        private System.Nullable<int> m_bufferView;
        
        /// <summary>
        /// Backing field for ByteOffset.
        /// </summary>
        private int m_byteOffset = 0;
        
        /// <summary>
        /// Backing field for ComponentType.
        /// </summary>
        private ComponentTypeEnum m_componentType;
        
        /// <summary>
        /// Backing field for Normalized.
        /// </summary>
        private bool m_normalized = false;
        
        /// <summary>
        /// Backing field for Count.
        /// </summary>
        private int m_count;
        
        /// <summary>
        /// Backing field for Type.
        /// </summary>
        private TypeEnum m_type;
        
        /// <summary>
        /// Backing field for Max.
        /// </summary>
        private float[] m_max;
        
        /// <summary>
        /// Backing field for Min.
        /// </summary>
        private float[] m_min;
        
        /// <summary>
        /// Backing field for Sparse.
        /// </summary>
        private AccessorSparse m_sparse;
        
        /// <summary>
        /// Backing field for Name.
        /// </summary>
        private string m_name;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> m_extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private Extras m_extras;
        
        /// <summary>
        /// The index of the bufferView.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("bufferView")]
        public System.Nullable<int> BufferView {
            get {
                return this.m_bufferView;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("BufferView", value, "Expected value to be greater than or equal to 0");
                }
                this.m_bufferView = value;
            }
        }
        
        /// <summary>
        /// The offset relative to the start of the bufferView in bytes.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("byteOffset")]
        public int ByteOffset {
            get {
                return this.m_byteOffset;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("ByteOffset", value, "Expected value to be greater than or equal to 0");
                }
                this.m_byteOffset = value;
            }
        }
        
        /// <summary>
        /// The datatype of components in the attribute.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("componentType")]
        public ComponentTypeEnum ComponentType {
            get {
                return this.m_componentType;
            }
            set {
                this.m_componentType = value;
            }
        }
        
        /// <summary>
        /// Specifies whether integer data values should be normalized.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("normalized")]
        public bool Normalized {
            get {
                return this.m_normalized;
            }
            set {
                this.m_normalized = value;
            }
        }
        
        /// <summary>
        /// The number of attributes referenced by this accessor.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("count")]
        public int Count {
            get {
                return this.m_count;
            }
            set {
                if ((value < 1)) {
                    throw new System.ArgumentOutOfRangeException("Count", value, "Expected value to be greater than or equal to 1");
                }
                this.m_count = value;
            }
        }
        
        /// <summary>
        /// Specifies if the attribute is a scalar, vector, or matrix.
        /// </summary>
        [Newtonsoft.Json.JsonConverterAttribute(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("type")]
        public TypeEnum Type {
            get {
                return this.m_type;
            }
            set {
                this.m_type = value;
            }
        }
        
        /// <summary>
        /// Maximum value of each component in this attribute.
        /// </summary>
        [Newtonsoft.Json.JsonConverterAttribute(typeof(glTFLoader.Shared.ArrayConverter))]
        [Newtonsoft.Json.JsonPropertyAttribute("max")]
        public float[] Max {
            get {
                return this.m_max;
            }
            set {
                if ((value == null)) {
                    this.m_max = value;
                    return;
                }
                if ((value.Length < 1u)) {
                    throw new System.ArgumentException("Array not long enough");
                }
                if ((value.Length > 16u)) {
                    throw new System.ArgumentException("Array too long");
                }
                this.m_max = value;
            }
        }
        
        /// <summary>
        /// Minimum value of each component in this attribute.
        /// </summary>
        [Newtonsoft.Json.JsonConverterAttribute(typeof(glTFLoader.Shared.ArrayConverter))]
        [Newtonsoft.Json.JsonPropertyAttribute("min")]
        public float[] Min {
            get {
                return this.m_min;
            }
            set {
                if ((value == null)) {
                    this.m_min = value;
                    return;
                }
                if ((value.Length < 1u)) {
                    throw new System.ArgumentException("Array not long enough");
                }
                if ((value.Length > 16u)) {
                    throw new System.ArgumentException("Array too long");
                }
                this.m_min = value;
            }
        }
        
        /// <summary>
        /// Sparse storage of attributes that deviate from their initialization value.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("sparse")]
        public AccessorSparse Sparse {
            get {
                return this.m_sparse;
            }
            set {
                this.m_sparse = value;
            }
        }
        
        /// <summary>
        /// The user-defined name of this object.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public string Name {
            get {
                return this.m_name;
            }
            set {
                this.m_name = value;
            }
        }
        
        /// <summary>
        /// Dictionary object with extension-specific objects.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("extensions")]
        public System.Collections.Generic.Dictionary<string, object> Extensions {
            get {
                return this.m_extensions;
            }
            set {
                this.m_extensions = value;
            }
        }
        
        /// <summary>
        /// Application-specific data.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("extras")]
        public Extras Extras {
            get {
                return this.m_extras;
            }
            set {
                this.m_extras = value;
            }
        }
        
        public bool ShouldSerializeBufferView() {
            return ((m_bufferView == null) 
                        == false);
        }
        
        public bool ShouldSerializeByteOffset() {
            return ((m_byteOffset == 0) 
                        == false);
        }
        
        public bool ShouldSerializeNormalized() {
            return ((m_normalized == false) 
                        == false);
        }
        
        public bool ShouldSerializeMax() {
            return ((m_max == null) 
                        == false);
        }
        
        public bool ShouldSerializeMin() {
            return ((m_min == null) 
                        == false);
        }
        
        public bool ShouldSerializeSparse() {
            return ((m_sparse == null) 
                        == false);
        }
        
        public bool ShouldSerializeName() {
            return ((m_name == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtensions() {
            return ((m_extensions == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtras() {
            return ((m_extras == null) 
                        == false);
        }
        
        public enum ComponentTypeEnum {
            
            BYTE = 5120,
            
            UNSIGNED_BYTE = 5121,
            
            SHORT = 5122,
            
            UNSIGNED_SHORT = 5123,
            
            UNSIGNED_INT = 5125,
            
            FLOAT = 5126,
        }
        
        public enum TypeEnum {
            
            SCALAR,
            
            VEC2,
            
            VEC3,
            
            VEC4,
            
            MAT2,
            
            MAT3,
            
            MAT4,
        }
    }
}
