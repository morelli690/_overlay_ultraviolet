﻿using System.Reflection;
using TwistedLogik.Nucleus.Text;

namespace TwistedLogik.Nucleus
{
    /// <summary>
    /// Contains Nucleus's string resources.
    /// </summary>
    public static class NucleusStrings
    {
        /// <summary>
        /// Initializes the NucleusStrings type.
        /// </summary>
        static NucleusStrings()
        {
            var asm = Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("TwistedLogik.Nucleus.Resources.Strings.xml"))
            {
                StringDatabase.LoadFromStream(stream);
            }
        }

        private static readonly LocalizationDatabase StringDatabase = new LocalizationDatabase();

#pragma warning disable 1591
        public static readonly StringResource MissingDefaultCtor                     = new StringResource(StringDatabase, "MISSING_DEFAULT_CTOR");
        public static readonly StringResource CtorMatchNotFound                      = new StringResource(StringDatabase, "CTOR_MATCH_NOT_FOUND");
        public static readonly StringResource CtorMatchIsAmbiguous                   = new StringResource(StringDatabase, "CTOR_MATCH_IS_AMBIGUOUS");
        public static readonly StringResource QueueIsEmpty                           = new StringResource(StringDatabase, "QUEUE_IS_EMPTY");
        public static readonly StringResource ListNodeDoesNotBelongToList            = new StringResource(StringDatabase, "LIST_NODE_DOES_NOT_BELONG_TO_LIST");
        public static readonly StringResource MinMustBeLessThanMax                   = new StringResource(StringDatabase, "MIN_MUST_BE_LESS_THAN_MAX");
        public static readonly StringResource InvalidWriteMethod                     = new StringResource(StringDatabase, "INVALID_WRITE_METHOD");
        public static readonly StringResource BufferLengthExceeded                   = new StringResource(StringDatabase, "BUFFER_LENGTH_EXCEEDED");
        public static readonly StringResource PoolImbalance                          = new StringResource(StringDatabase, "POOL_IMBALANCE");
        public static readonly StringResource PoolRegistryAlreadyContainsType        = new StringResource(StringDatabase, "POOL_REGISTRY_ALREADY_CONTAINS_TYPE");
        public static readonly StringResource CollectionChanged                      = new StringResource(StringDatabase, "COLLECTION_CHANGED");
        public static readonly StringResource UnrecognizedDataType                   = new StringResource(StringDatabase, "UNRECOGNIZED_DATA_TYPE");
        public static readonly StringResource UnableToDetectDataTypeFromExt          = new StringResource(StringDatabase, "UNABLE_TO_DETECT_DATA_TYPE_FROM_EXT");
        public static readonly StringResource InvalidDataObjectReference             = new StringResource(StringDatabase, "INVALID_DATA_OBJECT_REFERENCE");
        public static readonly StringResource DataObjectRegistryAlreadyExists        = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_ALREADY_EXISTS");
        public static readonly StringResource DataObjectRegistryDoesNotExist         = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_DOES_NOT_EXIST");
        public static readonly StringResource DataObjectRegistryCapacityExceeded     = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_CAPACITY_EXCEEDED");
        public static readonly StringResource DataObjectRegistryAlreadyContainsID    = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_ALREADY_CONTAINS_ID");
        public static readonly StringResource DataObjectRegistryMustLoadKeys         = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_MUST_LOAD_KEYS"); 
        public static readonly StringResource DataObjectRegistryAlreadyLoadedKeys    = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_ALREADY_LOADED_KEYS");
        public static readonly StringResource DataObjectRegistryAlreadyLoadedObjects = new StringResource(StringDatabase, "DATA_OBJECT_REGISTRY_ALREADY_LOADED_OBJECTS");
        public static readonly StringResource DataObjectMissingClass                 = new StringResource(StringDatabase, "DATA_OBJECT_MISSING_CLASS");
        public static readonly StringResource DataObjectMissingKey                   = new StringResource(StringDatabase, "DATA_OBJECT_MISSING_KEY");
        public static readonly StringResource DataObjectMissingID                    = new StringResource(StringDatabase, "DATA_OBJECT_MISSING_ID");
        public static readonly StringResource DataObjectInvalidType                  = new StringResource(StringDatabase, "DATA_OBJECT_INVALID_TYPE");
        public static readonly StringResource DataObjectIncompatibleType             = new StringResource(StringDatabase, "DATA_OBJECT_INCOMPATIBLE_TYPE");
        public static readonly StringResource DataObjectInvalidClass                 = new StringResource(StringDatabase, "DATA_OBJECT_INVALID_CLASS");
        public static readonly StringResource DataObjectInvalidClassAlias            = new StringResource(StringDatabase, "DATA_OBJECT_INVALID_CLASS_ALIAS");
        public static readonly StringResource DataObjectInvalidKey                   = new StringResource(StringDatabase, "DATA_OBJECT_INVALID_KEY");
        public static readonly StringResource DataObjectInvalidID                    = new StringResource(StringDatabase, "DATA_OBJECT_INVALID_IDS");
        public static readonly StringResource DataObjectPropertyIsReadOnly           = new StringResource(StringDatabase, "DATA_OBJECT_PROPERTY_IS_READ_ONLY");
        public static readonly StringResource DataObjectMissingIndexParam            = new StringResource(StringDatabase, "DATA_OBJECT_MISSING_INDEX_PARAM");
        public static readonly StringResource DataObjectHasTooManyIndexParams        = new StringResource(StringDatabase, "DATA_OBJECT_HAS_TOO_MANY_INDEX_PARAMS");
        public static readonly StringResource DataObjectAlreadySetDefaultAlias       = new StringResource(StringDatabase, "DATA_OBJECT_ALREADY_SET_DEFAULT_ALIAS");
        public static readonly StringResource DataObjectDefaultMissingClassName      = new StringResource(StringDatabase, "DATA_OBJECT_DEFAULT_MISSING_CLASS_NAME");
        public static readonly StringResource DataObjectDefaultHasInvalidClass       = new StringResource(StringDatabase, "DATA_OBJECT_DEFAULT_HAS_INVALID_CLASS");
        public static readonly StringResource DataObjectDefaultHasReservedKeyword    = new StringResource(StringDatabase, "DATA_OBJECT_DEFAULT_HAS_RESERVED_KEYWORD");
        public static readonly StringResource NonItemElementsInArrayDef              = new StringResource(StringDatabase, "NON_ITEM_ELEMENTS_IN_ARRAY_DEF");
        public static readonly StringResource NonItemElementsInListDef               = new StringResource(StringDatabase, "NON_ITEM_ELEMENTS_IN_LIST_DEF");
        public static readonly StringResource MemberDoesNotImplementList             = new StringResource(StringDatabase, "MEMBER_DOES_NOT_IMPLEMENT_LIST");
        public static readonly StringResource MemberImplementsMultipleListImpls      = new StringResource(StringDatabase, "MEMBER_IMPLEMENTS_MULTIPLE_LIST_IMPLS");
        public static readonly StringResource TypeAliasAlreadyRegistered             = new StringResource(StringDatabase, "TYPE_ALIAS_ALREADY_REGISTERED");
        public static readonly StringResource MessagePoolNotFound                    = new StringResource(StringDatabase, "MESSAGE_POOL_NOT_FOUND");
        public static readonly StringResource MessageMergeTypeConflict               = new StringResource(StringDatabase, "MESSAGE_MERGE_TYPE_CONFLICT");
        public static readonly StringResource FmtArgumentOutOfRange                  = new StringResource(StringDatabase, "FMT_ARGUMENT_OUT_OF_RANGE");
        public static readonly StringResource FmtCmdParseFailure                     = new StringResource(StringDatabase, "FMT_CMD_PARSE_FAILURE");
        public static readonly StringResource FmtCmdInvalidForArgument               = new StringResource(StringDatabase, "FMT_CMD_INVALID_FOR_ARGUMENT");
        public static readonly StringResource FmtCmdInvalidForGeneratedStrings       = new StringResource(StringDatabase, "FMT_CMD_INVALID_FOR_GENERATED_STRINGS");
        public static readonly StringResource SegmentIsEmpty                         = new StringResource(StringDatabase, "SEGMENT_IS_EMPTY");
        public static readonly StringResource LocalizedStringMissingKey              = new StringResource(StringDatabase, "LOCALIZED_STRING_MISSING_KEY");
#pragma warning restore 1591
    }
}
