#include "gtest/gtest.h"
#include "PixelMap.h"


TEST(PixelMap, WHEN_setValue_THEN_value_is_set)
{
  PixelMap _systemUnderTest(3,3, "Test");

  _systemUnderTest.setValue(0,0, "Color{0,0}");
  _systemUnderTest.setValue(0,1, "Color{0,1}");
  _systemUnderTest.setValue(1,0, "Color{1,0}");
  _systemUnderTest.setValue(1,1, "Color{1,1}");

  auto map = _systemUnderTest.getMap();

  EXPECT_EQ("Color{0,0}", map[0][0]);
  EXPECT_EQ("Color{0,1}", map[0][1]);
  EXPECT_EQ("Color{1,0}", map[1][0]);
  EXPECT_EQ("Color{1,1}", map[1][1]);
}

TEST(PixelMap, GIVEN_copy_constructor_WHEN_setValue_THEN_value_is_set)
{
  PixelMap _systemUnderTest(2,2, "Test");

  _systemUnderTest.setValue(0,0, "Color{0,0}");
  _systemUnderTest.setValue(0,1, "Color{0,1}");
  _systemUnderTest.setValue(1,0, "Color{1,0}");
  _systemUnderTest.setValue(1,1, "Color{1,1}");

  PixelMap _duplicate(_systemUnderTest);
  _duplicate.setValue(1,1, "Color{1,1}-duplicated");

  auto originalMap = _systemUnderTest.getMap();
  auto duplicateMap = _duplicate.getMap();

  EXPECT_EQ("Color{0,0}", originalMap[0][0]);
  EXPECT_EQ("Color{0,1}", originalMap[0][1]);
  EXPECT_EQ("Color{1,0}", originalMap[1][0]);
  EXPECT_EQ("Color{1,1}", originalMap[1][1]);

  EXPECT_EQ("Color{0,0}", duplicateMap[0][0]);
  EXPECT_EQ("Color{0,1}", duplicateMap[0][1]);
  EXPECT_EQ("Color{1,0}", duplicateMap[1][0]);
  EXPECT_EQ("Color{1,1}-duplicated", duplicateMap[1][1]);
}

TEST(PixelMap, GIVEN_out_of_range_value_WHEN_setValue_THEN_no_exception_thrown)
{
  PixelMap _systemUnderTest(2,2, "Test");

  _systemUnderTest.setValue(-1,-1, "Test1");
  _systemUnderTest.setValue(2,2, "Test2");
  _systemUnderTest.setValue(3,3, "Test3");
}

TEST(PixelMap, GIVEN_heightDimension_WHEN_getMap_THEN_dimensions_are_correct)
{
  /**
   * [Test] [Test]
   * [Test] [Test]
   * [Test] [Test]
   * [Test] [Test]
   * [Test] [Expected]
   */
  PixelMap _systemUnderTest(2,5, "Test");

  _systemUnderTest.setValue(1,4, "Expected");

  auto map = _systemUnderTest.getMap();

  EXPECT_EQ("Expected", map[1][4]);
}

TEST(PixelMap, GIVEN_widthDimension_WHEN_getMap_THEN_dimensions_are_correct)
{
  /**
   * [Test] [Test] [Test] [Test] [Test]
   * [Test] [Test] [Test] [Test] [Expected]
   */
  PixelMap _systemUnderTest(5,2, "Test");

  _systemUnderTest.setValue(4,1, "Expected");

  auto map = _systemUnderTest.getMap();

  EXPECT_EQ("Expected", map[4][1]);
}

int main(int argc, char **argv) {
  ::testing::InitGoogleTest(&argc, argv);
  return RUN_ALL_TESTS();
}