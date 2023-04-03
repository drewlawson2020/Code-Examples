#include "gtest/gtest.h"
#include "AnimationFrame.h"
#include "PixelMap.h"

TEST(AnimationFrameTest, WHEN_AddLayer_THEN_layerId_returned_correctly)
{
  AnimationFrame _systemUnderTest;

  int layer1Id = _systemUnderTest.addLayer(10,10);
  int layer2Id = _systemUnderTest.addLayer(10,10);
  int layer3Id = _systemUnderTest.addLayer(10,10);

  EXPECT_EQ(0, layer1Id);
  EXPECT_EQ(1, layer2Id);
  EXPECT_EQ(2, layer3Id);
}

TEST(AnimationFrameTest, WHEN_getLayer_THEN_layer_returned_correctly)
{
  AnimationFrame _systemUnderTest;

  int layer1Id = _systemUnderTest.addLayer(10,10);

  auto layer1 = _systemUnderTest.getLayer(layer1Id);

  layer1.setValue(1,1, "TEST");

  auto actual = layer1.getMap();

  EXPECT_EQ("TEST", actual[1][1]);
}


TEST(AnimationFrameTest, WHEN_getMergedLayers_THEN_pixelMap_is_correct)
{
  AnimationFrame _systemUnderTest;

  int layer1Id = _systemUnderTest.addLayer(10,10);
  int layer2Id = _systemUnderTest.addLayer(10,10);

  PixelMap& layer1 = _systemUnderTest.getLayer(layer1Id);
  layer1.setValue(0,0, "Layer1-Replaced");
  layer1.setValue(1,1, "Layer1");

  PixelMap& layer2 = _systemUnderTest.getLayer(layer2Id);
  layer2.setValue(0,0, "Layer2");
  layer2.setValue(2,2, "Layer2");

  PixelMap pixelMap = _systemUnderTest.getMergedLayers();

  auto actual = pixelMap.getMap();

  EXPECT_EQ("Layer2", actual[0][0]);
  EXPECT_EQ("Layer1", actual[1][1]);
  EXPECT_EQ("Layer2", actual[2][2]);
}


int main(int argc, char **argv) {
  ::testing::InitGoogleTest(&argc, argv);
  return RUN_ALL_TESTS();
}